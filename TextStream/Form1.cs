using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TextStream
{
    public partial class Form1 : Form
    {
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        Thread networkThread;
        bool isServer = false;
        bool suppressTextChange = false;
        private System.Windows.Forms.Timer sendTimer;
        private volatile bool isRunning = false;
        private List<TcpClient> clients = new List<TcpClient>();
        private object clientLock = new object();
        private HashSet<string> recentlySentFiles = new HashSet<string>();
        private bool allowClientSend = true;

        public Form1()
        {
            InitializeComponent();
            cmbMode.Items.AddRange(new[] { "Server", "Client" });
            cmbMode.SelectedIndex = 0;
            txtPort.Text = "8888";
            txtServerIP.Text = GetLocalIPAddress();
            lblCurrentFile.Text = "No file streaming yet...";
            chkAllowClientSend.Checked = true; // Default to allow client send
            button1.Enabled = false;
            this.AllowDrop = false;
            chkAllowClientSend.Visible = cmbMode.SelectedItem?.ToString() == "Server";
            SetupDebounce();
        }

        private bool TryGetPort(out int port)
        {
            port = 8888; // default
            if (int.TryParse(txtPort.Text, out int inputPort) && inputPort >= 1024 && inputPort <= 65535)
            {
                port = inputPort;
                return true;
            }

            MessageBox.Show("Invalid port number. Please enter a number between 1024 and 65535.", "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                isServer = cmbMode.SelectedItem.ToString() == "Server";

                if (isServer)
                {
                    txtServerIP.Text = GetLocalIPAddress();
                    StartServer();
                }
                else
                {
                    StartClient(txtServerIP.Text);
                }


            }
            else // Disconnect
            {
                Disconnect();
            }
        }

        void Disconnect()
        {
            Cleanup();
            AppendStatus("Disconnected.");

            cmbMode.Enabled = true;
            txtServerIP.Enabled = true;
            txtPort.Enabled = true;
            chkAllowClientSend.Enabled = true;
            button1.Enabled = false;
            this.AllowDrop = false;
            btnStart.Text = "Start";
            btnStart.ForeColor = Color.Green;

        }

        private void Cleanup()
        {
            isRunning = false;

            try { stream?.Close(); } catch { }
            try { client?.Close(); } catch { }

            if (server != null)
            {
                try { server.Stop(); } catch { }
                server = null;
            }

            lock (clientLock)
            {
                foreach (var c in clients)
                {
                    try { c.Close(); } catch { }
                }
                clients.Clear();
            }
        }


        void StartServer()
        {
            try
            {
                if (!TryGetPort(out int port)) return;

                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                isRunning = true;
                AppendStatus("Server started at " + GetLocalIPAddress() + ":" + port);

                cmbMode.Enabled = false;
                txtServerIP.Enabled = false;
                txtPort.Enabled = false;
                chkAllowClientSend.Enabled = false;
                button1.Enabled = true;
                this.AllowDrop = true;
                btnStart.Text = "Disconnect";
                btnStart.ForeColor = Color.Red;

                new Thread(() =>
                {
                    while (isRunning)
                    {
                        try
                        {
                            TcpClient newClient = server.AcceptTcpClient();
                            lock (clientLock)
                            {
                                clients.Add(newClient);
                            }

                            bool allowClientSend = chkAllowClientSend.Checked;
                            var writer = new BinaryWriter(newClient.GetStream(), Encoding.UTF8, true);
                            writer.Write((byte)0x03); // control message type
                            writer.Write(allowClientSend); // send server's checkbox state
                            writer.Flush();

                            AppendStatus("New client connected: " + newClient.Client.RemoteEndPoint.ToString());
                            new Thread(() => ReadClientStream(newClient)) { IsBackground = true }.Start();
                        }
                        catch
                        {
                            break; // Server stopped
                        }
                    }
                })
                { IsBackground = true }.Start();
            }catch (SocketException ex)
            {
                AppendStatus("Another instance is already running on the same Port...");
            }
        }




        void StartClient(string ip)
        {
            if (!TryGetPort(out int port)) return;

            try
            {
                isRunning = true;
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);

                stream = client.GetStream();
                new Thread(() => ReadServerStream(stream)) { IsBackground = true }.Start();

                AppendStatus("Connected to server.");

                cmbMode.Enabled = false;
                txtServerIP.Enabled = false;
                txtPort.Enabled = false;
                chkAllowClientSend.Enabled = false;
                btnStart.Text = "Disconnect";
                btnStart.ForeColor = Color.Red;

            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    AppendStatus("Failed to connect: Server found, but port is not open or server is not listening on the specified port.");
                }
                else if (ex.SocketErrorCode == SocketError.HostNotFound)
                {
                    AppendStatus("Failed to connect: Host not found.");
                }
                else
                {
                    AppendStatus($"Socket error ({ex.SocketErrorCode}): {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                AppendStatus("Connection error: " + ex.Message);
            }
        }





        private void txtStream_TextChanged(object sender, EventArgs e)
        {

            if (suppressTextChange) return;
            if (sendTimer != null)
            {
                sendTimer.Stop();
                sendTimer.Start();
            }
        }

        private void SetupDebounce()
        {
            sendTimer = new System.Windows.Forms.Timer();
            sendTimer.Interval = 300; // milliseconds delay
            sendTimer.Tick += (s, e) =>
            {
                sendTimer.Stop();
                SendText(txtStream.Text);
            };
        }


        private void AppendStatus(string msg)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => AppendStatus(msg)));
                return;
            }

            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private string GetLocalIPAddress()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return "127.0.0.1";
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {

            if (!isServer && !allowClientSend)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!isServer && !allowClientSend)
            {
                AppendStatus("File sending is disabled by the server.");
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                SendFile(file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SendFile(ofd.FileName);
                }
            }
        }

        private void SendText(string content)
        {
            if (isServer)
            {
                lock (clientLock)
                {
                    foreach (var client in clients.ToList())
                    {
                        try
                        {
                            if (client.Connected)
                            {
                                var writer = new BinaryWriter(client.GetStream(), Encoding.UTF8, true);
                                writer.Write((byte)0x01);
                                writer.Write(content);
                                writer.Flush();
                            }
                            else clients.Remove(client);
                        }
                        catch { clients.Remove(client); }
                    }
                }
            }
            else
            {
                if (stream == null || !stream.CanWrite) return;
                try
                {
                    var writer = new BinaryWriter(stream, Encoding.UTF8, true);
                    writer.Write((byte)0x01);
                    writer.Write(content);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    AppendStatus("Client send error: " + ex.Message);

                }
            }
        }
        private void SendFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            byte[] fileData = File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);

            // ✅ Mark this file as recently sent (to suppress SaveFile)
            recentlySentFiles.Add(fileName);

            if (isServer)
            {
                lock (clientLock)
                {
                    foreach (var client in clients.ToList())
                    {
                        try
                        {
                            if (client.Connected)
                            {
                                var writer = new BinaryWriter(client.GetStream(), Encoding.UTF8, true);
                                writer.Write((byte)0x02);
                                writer.Write(fileNameBytes.Length);
                                writer.Write(fileNameBytes);
                                writer.Write(fileData.Length);
                                writer.Write(fileData);
                                writer.Flush();
                            }
                            else clients.Remove(client);
                        }
                        catch { clients.Remove(client); }
                    }
                }
            }
            else
            {
                if (stream == null || !stream.CanWrite) return;
                try
                {
                    var writer = new BinaryWriter(stream, Encoding.UTF8, true);
                    writer.Write((byte)0x02);
                    writer.Write(fileNameBytes.Length);
                    writer.Write(fileNameBytes);
                    writer.Write(fileData.Length);
                    writer.Write(fileData);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    AppendStatus("Client file send error: " + ex.Message);
                }
            }

            Invoke((MethodInvoker)(() =>
            {
                lblCurrentFile.Text = "Streaming file: " + fileName;
            }));
        }


        private void ReadClientStream(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
                while (client.Connected)
                {
                    byte messageType = reader.ReadByte(); // 0x01 for text, 0x02 for file

                    if (messageType == 0x01)
                    {
                        string text = reader.ReadString();
                        BroadcastText(text, client);
                    }
                    else if (messageType == 0x02)
                    {
                        int fileNameLen = reader.ReadInt32();
                        string fileName = Encoding.UTF8.GetString(reader.ReadBytes(fileNameLen));
                        int fileLength = reader.ReadInt32();
                        byte[] fileData = reader.ReadBytes(fileLength);

                        SaveFile(fileName, fileData);
                        BroadcastFile(fileName, fileData, client);
                    }
                }
            }
            catch (Exception ex)
            {
                //AppendStatus("Client disconnected: " + client.Client.RemoteEndPoint);
                clients.Remove(client);


                if (clients.Count == 0)
                {
                    AppendStatus("All clients have disconnected.");
                }
                else
                {
                    AppendStatus($"Remaining connected clients: {clients.Count}");
                }
            }
        }
        private void BroadcastText(string message, TcpClient sender)
        {
            Invoke((MethodInvoker)(() =>
            {
                suppressTextChange = true;
                txtStream.Text = message;
                suppressTextChange = false;
            }));

            lock (clientLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (c != sender && c.Connected)
                    {
                        try
                        {
                            NetworkStream ns = c.GetStream();
                            BinaryWriter writer = new BinaryWriter(ns, Encoding.UTF8, true);
                            writer.Write((byte)0x01);
                            writer.Write(message);
                            writer.Flush();
                        }
                        catch { clients.Remove(c); }
                    }
                }
            }
        }
        private void BroadcastFile(string fileName, byte[] fileData, TcpClient sender)
        {
            Invoke((MethodInvoker)(() =>
            {
                lblCurrentFile.Text = "Received file from Client: " + fileName;
                var remoteEndPoint = sender.Client.RemoteEndPoint?.ToString();
                AppendStatus($"Received file from Client ({remoteEndPoint}): {fileName}");
            }));

            lock (clientLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (c != sender && c.Connected)
                    {
                        try
                        {
                            NetworkStream ns = c.GetStream();
                            BinaryWriter writer = new BinaryWriter(ns, Encoding.UTF8, true);
                            writer.Write((byte)0x02);
                            writer.Write(fileName.Length);
                            writer.Write(Encoding.UTF8.GetBytes(fileName));
                            writer.Write(fileData.Length);
                            writer.Write(fileData);
                            writer.Flush();
                        }
                        catch { clients.Remove(c); }
                    }
                }
            }
        }

        private void ReadServerStream(NetworkStream stream)
        {
            try
            {
                BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
                while (isRunning && stream.CanRead)
                {
                    byte type = reader.ReadByte();

                    if (type == 0x01) // Text
                    {
                        string text = reader.ReadString();
                        Invoke((MethodInvoker)(() =>
                        {
                            suppressTextChange = true;
                            txtStream.Text = text;
                            suppressTextChange = false;
                        }));
                    }
                    else if (type == 0x02) // File
                    {
                        int fileNameLen = reader.ReadInt32();
                        string fileName = Encoding.UTF8.GetString(reader.ReadBytes(fileNameLen));
                        int fileLength = reader.ReadInt32();
                        byte[] fileData = reader.ReadBytes(fileLength);

                        SaveFile(fileName, fileData);

                        Invoke((MethodInvoker)(() =>
                        {
                            lblCurrentFile.Text = "Received file: " + fileName;
                            AppendStatus("Received file from Server: " + fileName);
                        }));
                    }
                    else if (type == 0x03) // Settings
                    {
                        allowClientSend = reader.ReadBoolean();
                        Invoke((MethodInvoker)(() =>
                        {
                            txtStream.ReadOnly = !allowClientSend;
                            button1.Enabled = allowClientSend;
                            this.AllowDrop = allowClientSend;
                            btnClear.Enabled = allowClientSend;
                            lblCurrentFile.Text = allowClientSend
                                ? "Ready to stream files."
                                : "Client is not allowed to stream text or files.";
                        }));
                    }
                }
            }
            catch (IOException)
            {
                // Connection closed cleanly, no action needed
            }
            catch (ObjectDisposedException)
            {
                // Stream was disposed due to disconnect
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => AppendStatus("Read error: " + ex.Message)));

            }
            finally
            {
                OnServerDisconnected();
            }


        }

        private void OnServerDisconnected()
        {
            isRunning = false;

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)OnServerDisconnected);
                return;
            }

            AppendStatus("Disconnected from server.");
            Cleanup(); // Clear all network resources

            cmbMode.Enabled = true;
            txtServerIP.Enabled = true;
            txtPort.Enabled = true;
            chkAllowClientSend.Enabled = true;
            btnClear.Enabled = true;
            button1.Enabled = false;
            this.AllowDrop = false;
            btnStart.Text = "Start";
            btnStart.ForeColor = Color.Green;
        }

        private void SaveFile(string fileName, byte[] fileData)
        {
            if (recentlySentFiles.Contains(fileName))
            {
                recentlySentFiles.Remove(fileName); // Remove it once used
                return; // Don't save or open folder for self-sent files
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string targetFolder = Path.Combine(documentsPath, "ReceivedStreamedFiles");
            Directory.CreateDirectory(targetFolder);

            string savePath = Path.Combine(targetFolder, fileName);
            File.WriteAllBytes(savePath, fileData);

            System.Diagnostics.Process.Start("explorer.exe", targetFolder);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cleanup();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog.Title = "Save chat log";
                saveFileDialog.FileName = "TextStream_Log.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, txtStream.Text);
                        MessageBox.Show("File saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to save file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStream.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtStream.CanUndo)
            {
                txtStream.Undo();
            }
            else
            {
                MessageBox.Show("Nothing to undo.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMode.SelectedItem.ToString() == "Server")
            {
                chkAllowClientSend.Visible = true;
            }
            else // Client mode
            {
                chkAllowClientSend.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isServer) return;

            bool allow = chkAllowClientSend.Checked;
            lock (clientLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (c.Connected)
                    {
                        try
                        {
                            var writer = new BinaryWriter(c.GetStream(), Encoding.UTF8, true);
                            writer.Write((byte)0x03);
                            writer.Write(allow);
                            writer.Flush();
                        }
                        catch { clients.Remove(c); }
                    }
                }
            }
        }
    }
}
