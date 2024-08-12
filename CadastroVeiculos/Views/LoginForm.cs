using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroVeiculos.Views
{
    public partial class LoginForm : Form
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public LoginForm()
        {
            InitializeComponent();
            ConfigurePasswordTextBox(); // Configura a caixa de senha para esconder o texto
        }

        private void ConfigurePasswordTextBox()
        {
            txtSenha.PasswordChar = '*'; // Isso define o caractere de máscara como '*'
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Qualquer lógica que precise ser executada ao carregar o formulário
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool loginSuccessful = await TryLoginAsync(txtUsuario.Text, txtSenha.Text);

                if (loginSuccessful)
                {
                    OpenMainForm();
                }
                else
                {
                    ShowLoginError();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private async Task<bool> TryLoginAsync(string username, string password)
        {
            string apiUrl = "https://test-api-y04b.onrender.com/signIn";
            var loginData = new
            {
                user = username,
                password = password
            };

            string jsonData = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return response.IsSuccessStatusCode;
        }

        private void OpenMainForm()
        {
            this.Hide();  // Esconde o LoginForm
            MainForm mainForm = new MainForm();
            mainForm.Show();  // Mostra o MainForm
        }

        private void ShowLoginError()
        {
            MessageBox.Show("Usuário ou senha incorretos.");
        }

        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show("Erro ao fazer login: " + ex.Message);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ClearLoginForm();
        }

        private void ClearLoginForm()
        {
            txtUsuario.Clear();
            txtSenha.Clear();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // Logica para quando o texto do usuario for alterado
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // Logica para quando o texto da senha for alterado
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Logica para quando o label1 for clicado
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Logica para quando o label2 for clicado
        }
    }
}
