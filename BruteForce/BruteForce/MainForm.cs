namespace BruteForce
{
    public partial class MainForm : Form
    {
        private PasswordManager _passwordManager;
        private AttackController _attackController;
        private string? _currentTargetHash;

        public MainForm()
        {
            InitializeComponent();

            _passwordManager = new PasswordManager();
            _attackController = new AttackController();

            _attackController.OnPasswordFound = HandlePasswordFound;
            _attackController.OnFinished = HandleFinished;
        }

        private void BtnGenerate_Click(object? sender, EventArgs e)
        {
            _currentTargetHash = _passwordManager.GenerateTargetHash(out string actualPassword);

            lblTargetHash.Text = $"Target Hash: {_currentTargetHash.Substring(0, 15)}... (Length: {actualPassword.Length})";
            txtLog.AppendText($"\r\n[{DateTime.Now:HH:mm:ss}] Generated new target password. Ready for attack.\r\n");

            btnStartSingle.Enabled = true;
            btnStartMulti.Enabled = true;
        }

        private async void BtnStartSingle_Click(object? sender, EventArgs e)
        {
            await StartAttack(false);
        }

        private async void BtnStartMulti_Click(object? sender, EventArgs e)
        {
            await StartAttack(true);
        }

        private async Task StartAttack(bool multiThreaded)
        {
            ToggleButtons(false);
            lblStatus.Text = "Status: Attacking...";
            lblElapsedTime.Text = "Elapsed Time: 0.00 s";

            progressBar.Style = ProgressBarStyle.Marquee;

            string mode = multiThreaded ? "Multi-Threaded" : "Single-Threaded";
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Starting {mode} Attack...\r\n");

            if (_currentTargetHash != null)
            {
                await _attackController.StartAttackAsync(_currentTargetHash, multiThreaded);
            }
        }

        private void BtnStop_Click(object? sender, EventArgs e)
        {
            _attackController.StopAttack();
            lblStatus.Text = "Status: Force Stopped.";
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Attack cancelled by user.\r\n");
            ResetProgress();
        }

        private void HandlePasswordFound(string password)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandlePasswordFound), password);
                return;
            }

            txtLog.AppendText($"\r\n*** SUCCESS: Password found! -> {password} ***\r\n");
            lblStatus.Text = "Status: Password Found!";
            ResetProgress();
        }

        private void HandleFinished(TimeSpan elapsed)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TimeSpan>(HandleFinished), elapsed);
                return;
            }

            lblElapsedTime.Text = $"Elapsed Time: {elapsed.TotalSeconds:F2} seconds";
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Task ended. Total time: {elapsed.TotalSeconds:F2}s\r\n");

            ToggleButtons(true);
            ResetProgress();
        }

        private void ToggleButtons(bool isIdle)
        {
            btnGenerate.Enabled = isIdle;
            btnStartSingle.Enabled = isIdle && !string.IsNullOrEmpty(_currentTargetHash);
            btnStartMulti.Enabled = isIdle && !string.IsNullOrEmpty(_currentTargetHash);
            btnStop.Enabled = !isIdle;
        }

        private void ResetProgress()
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = 0;
        }
    }
}