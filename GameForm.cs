﻿using System;
using System.Drawing;
using System.Windows.Forms;
using BoardGameWinForms;
using chế_độ_cổ_điển; // Thêm using cho namespace của MainForm1
namespace WindowsFormsApp10
{
    public partial class GameForm : Form
    {
        private Button btnClassicMode, btnChallengeMode, btnTwoPlayerMode;
        private GameMode currentGameMode;
        private LoginForm loginForm;
        private UserManager userManager; // Add this line
        private MainForm1 challengeFormInstance; // Thêm biến thành viên
        private MainForm mainForm; // Thêm biến thành viên
        public GameForm(MainForm mainFormInstance, LoginForm loginFormInstance, UserManager userManagerInstance)
        {
            this.mainForm = mainFormInstance; // Lưu instance của MainForm
            this.loginForm = loginFormInstance;
            this.userManager = userManagerInstance;

            SetupGameModeSelection();

            this.FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Application.Exit();
                }
            };
        }
        // Enum for game modes
        public enum GameMode
        {
            Classic,
            Challenge,
            TwoPlayer
        }

        public GameForm()
        {
            
            SetupGameModeSelection();
            this.FormClosing += (s, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Application.Exit(); // Đóng toàn bộ ứng dụng
                }
            };
        }

        private void SetupGameModeSelection()
        {
            // Configure form
            this.Text = "Chọn Chế Độ Chơi";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title Label
            Label lblTitle = new Label
            {
                Text = "Chọn Chế Độ Chơi",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(200, 50)
            };
            this.Controls.Add(lblTitle);

            // Classic Mode Button
            btnClassicMode = CreateModeButton("Chế Độ Cổ Điển", 150, 150);
            btnClassicMode.Click += (s, e) => StartGame(GameMode.Classic);

            // Challenge Mode Button
            btnChallengeMode = CreateModeButton("Chế Độ Vượt Thử Thách", 150, 250);
            btnChallengeMode.Click += (s, e) => {
                if (challengeFormInstance == null || challengeFormInstance.IsDisposed)
                {
                    challengeFormInstance = new MainForm1();
                }
                this.Hide();
                challengeFormInstance.ShowDialog();
                this.Show();
            };

            // Two Player Mode Button
            btnTwoPlayerMode = CreateModeButton("Chế Độ Hai Người Chơi", 150, 350);
            btnTwoPlayerMode.Click += (s, e) => StartGame(GameMode.TwoPlayer);

            // Back Button
            Button btnBack = new Button
            {
                Text = "Quay Lại",
                Location = new Point(250, 450),
                Size = new Size(100, 40)
            };
            btnBack.Click += (s, e) =>
            {
                this.Close();
                mainForm.Show(); // Hiển thị lại MainForm
            };

            // Add controls
            this.Controls.Add(btnClassicMode);
            this.Controls.Add(btnChallengeMode);
            this.Controls.Add(btnTwoPlayerMode);
            this.Controls.Add(btnBack);
        }

        private Button CreateModeButton(string text, int x, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(300, 50),
                Font = new Font("Arial", 12)
            };
        }

        private void StartGame(GameMode mode)
        {
            currentGameMode = mode;
            GameForm gameFormInstance = this;
            // Tạo form chơi game với chế độ đã chọn
            Form gameplayForm = new Form
            {
                Text = GetGameModeTitle(mode),
                Size = new Size(1000, 800),
                StartPosition = FormStartPosition.CenterScreen
            };

            // Tạo BoardManager và các thành phần game chính
            BoardManager boardManager = new BoardManager();
            Player player1 = new Player();
            Player player2 = new Player();

            // Tùy chỉnh game theo từng chế độ
            switch (mode)
            {
                case GameMode.Classic:
                    ConfigureClassicMode(gameplayForm,  gameFormInstance);
                    break;
                case GameMode.Challenge:
                    ConfigureChallengeMode(gameplayForm, boardManager, player1, player2, gameFormInstance);
                    break;
                case GameMode.TwoPlayer:
                    // Khởi tạo GameForm từ BoardGameWinForms
                    BoardGameWinForms.GameForm game = new BoardGameWinForms.GameForm();
                    this.Hide(); // Ẩn form chọn chế độ
                    game.ShowDialog(); // Hiển thị form game dưới dạng modal dialog
                    this.Show(); // Hiển thị lại form chọn chế độ sau khi form game đóng

                    // Không cần cấu hình thêm gì nữa, vì game đã tự xử lý
                    return; // Thoát khỏi hàm StartGame
                    
            }

            // Ẩn form chọn chế độ
            gameplayForm.FormClosed += (s, e) =>
            {
                this.Show(); // Hiển thị GameForm khi gameplayForm đóng
                // Hoặc this.Close(); nếu bạn muốn đóng toàn bộ ứng dụng
            };
            gameplayForm.Show();



        }

        private string GetGameModeTitle(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Classic:
                    return "Chế Độ Cổ Điển";
                case GameMode.Challenge:
                    return "Chế Độ Vượt Thử Thách";
                case GameMode.TwoPlayer:
                    return "Chế Độ Hai Người Chơi";
                default:
                    return "Trò Chơi";
            }
        }

        private void ConfigureClassicMode(Form gameForm, GameForm gameFormInstance)
        {
            // Khởi tạo và hiển thị Form3
            Form3 form3 = new Form3();
            gameForm.Hide(); // Ẩn form chọn chế độ
            form3.ShowDialog();
            gameFormInstance.Show();
        }

        private void ConfigureChallengeMode(Form gameForm, BoardManager boardManager, Player player1, Player player2, GameForm gameFormInstance)
        {
            
            Label lblChallengeModeInfo = new Label
            {
                Text = "Chế Độ Vượt Thử Thách: Độ khó cao, HP giảm",
                Location = new Point(50, 50),
                AutoSize = true
            };
            gameForm.Controls.Add(lblChallengeModeInfo);

            // Điều chỉnh HP ban đầu
            
            CreateBackButton(gameForm, gameFormInstance);
        }

        private void ConfigureTwoPlayerMode(Form gameForm, BoardManager boardManager, Player player1, Player player2, GameForm gameFormInstance)
        {
            // Cài đặt cho chế độ 2 người chơi
            // Ví dụ: Không có quy tắc đặc biệt
            Label lblTwoPlayerModeInfo = new Label
            {
                Text = "Chế Độ Hai Người Chơi: Trận đấu trực tiếp",
                Location = new Point(50, 50),
                AutoSize = true
            };
            gameForm.Controls.Add(lblTwoPlayerModeInfo);
            CreateBackButton(gameForm, gameFormInstance);
        }
        private void CreateBackButton(Form gameForm, GameForm gameFormInstance)
        {
            Button btnBack = new Button
            {
                Text = "Quay Lại",
                Location = new Point(10, 10), // Điều chỉnh vị trí nếu cần
                Size = new Size(80, 30)
            };

            btnBack.Click += (s, e) =>
            {
                gameForm.Close();
                gameFormInstance.Show();
            };

            gameForm.Controls.Add(btnBack);
        }
    }
}