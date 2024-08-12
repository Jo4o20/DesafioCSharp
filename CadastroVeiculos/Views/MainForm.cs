using CadastroVeiculos.Data;
using CadastroVeiculos.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroVeiculos.Views
{
    public partial class MainForm : Form
    {
        private MySqlConnection _conexao;
        private static readonly HttpClient _httpClient = new HttpClient();

        public MainForm()
        {
            InitializeComponent();
            RegisterEventHandlers();
            ObterMarcas();  // Carrega as marcas ao iniciar o formulário
        }

        private void RegisterEventHandlers()
        {
            // Eventos dos botões e do DataGridView
            dgvVeiculos.CellClick += dgvVeiculos_CellClick;
            btnEditar.Click += btnEditar_Click;
            btnExcluir.Click += btnExcluir_Click;
            btnAdicionar.Click += btnAdicionar_Click;
            btnLimpar.Click += btnLimpar_Click;
            cmbMarcas.SelectedIndexChanged += cmbMarcas_SelectedIndexChanged;  // Adiciona o evento para quando a marca é selecionada
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadVeiculos();  // Carrega os veículos ao iniciar o formulário
        }

        private void LoadVeiculos()
        {
            try
            {
                _conexao = ConexaoBD.GetConnection();

                if (_conexao != null)
                {
                    string query = "SELECT * FROM Veiculos";
                    using (MySqlCommand cmd = new MySqlCommand(query, _conexao))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable veiculosTable = new DataTable();
                        da.Fill(veiculosTable);
                        dgvVeiculos.DataSource = veiculosTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar veículos: {ex.Message}");
            }
            finally
            {
                _conexao?.Close(); // Fecha a conexão após o uso
            }
        }

        private Veiculo ObterVeiculoDoFormulario()
        {
            return new Veiculo
            {
                Placa = txtPlaca.Text,
                Chassi = txtChassi.Text,
                Marca = cmbMarcas.Text,  // Obtém o nome da marca selecionada no ComboBox
                Modelo = string.Empty,  // Sem uso, manter como string vazia ou remover da inserção no banco
                AnoFabricacao = int.Parse(txtAnoFabricacao.Text),
                AnoModelo = int.Parse(txtAnoModelo.Text),
                ValorFipe = decimal.Parse(txtValorFipe.Text),
                ValorVenda = decimal.Parse(txtValorVenda.Text),
                Observacoes = txtObservacoes.Text
            };
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ExecuteNonQueryOnVeiculo(
                "INSERT INTO Veiculos (Placa, Chassi, Marca, Modelo, AnoFabricacao, AnoModelo, ValorFipe, ValorVenda, Observacoes) " +
                "VALUES (@Placa, @Chassi, @Marca, @Modelo, @AnoFabricacao, @AnoModelo, @ValorFipe, @ValorVenda, @Observacoes)",
                "Veículo adicionado com sucesso!");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvVeiculos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvVeiculos.SelectedRows[0].Cells["Id"].Value);

                ExecuteNonQueryOnVeiculo(
                    "UPDATE Veiculos SET Placa = @Placa, Chassi = @Chassi, Marca = @Marca, Modelo = @Modelo, " +
                    "AnoFabricacao = @AnoFabricacao, AnoModelo = @AnoModelo, ValorFipe = @ValorFipe, ValorVenda = @ValorVenda, Observacoes = @Observacoes " +
                    "WHERE Id = @Id",
                    "Veículo atualizado com sucesso!",
                    id);

                LoadVeiculos(); // Recarrega os dados após a edição
                dgvVeiculos.ClearSelection(); // Limpa a seleção após a edição
                ClearForm(); // Limpa os campos após a edição
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvVeiculos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvVeiculos.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este veículo?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ExecuteNonQueryOnVeiculo(
                        "DELETE FROM Veiculos WHERE Id = @Id",
                        "Veículo excluído com sucesso!",
                        id);
                }
            }
        }

        private void ExecuteNonQueryOnVeiculo(string query, string successMessage, int? id = null)
        {
            try
            {
                _conexao = ConexaoBD.GetConnection();

                if (_conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, _conexao))
                    {
                        Veiculo veiculo = ObterVeiculoDoFormulario();

                        cmd.Parameters.AddWithValue("@Placa", veiculo.Placa);
                        cmd.Parameters.AddWithValue("@Chassi", veiculo.Chassi);
                        cmd.Parameters.AddWithValue("@Marca", veiculo.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", veiculo.Modelo);  // Pode ser removido ou substituído
                        cmd.Parameters.AddWithValue("@AnoFabricacao", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@AnoModelo", veiculo.AnoModelo);
                        cmd.Parameters.AddWithValue("@ValorFipe", veiculo.ValorFipe);
                        cmd.Parameters.AddWithValue("@ValorVenda", veiculo.ValorVenda);
                        cmd.Parameters.AddWithValue("@Observacoes", veiculo.Observacoes);

                        if (id.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@Id", id.Value);
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(successMessage);
                        LoadVeiculos(); // Atualiza a lista de veículos
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                _conexao?.Close(); // Fecha a conexão após o uso
            }
        }

        private void dgvVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVeiculos.Rows[e.RowIndex];

                txtPlaca.Text = row.Cells["Placa"].Value.ToString();
                txtChassi.Text = row.Cells["Chassi"].Value.ToString();
                cmbMarcas.Text = row.Cells["Marca"].Value.ToString();  // Atualiza o ComboBox de marcas
                txtAnoFabricacao.Text = row.Cells["AnoFabricacao"].Value.ToString();
                txtAnoModelo.Text = row.Cells["AnoModelo"].Value.ToString();
                txtValorFipe.Text = row.Cells["ValorFipe"].Value.ToString();
                txtValorVenda.Text = row.Cells["ValorVenda"].Value.ToString();
                txtObservacoes.Text = row.Cells["Observacoes"].Value.ToString();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ClearForm(); // Limpa os campos do formulário
        }

        private void ClearForm()
        {
            txtPlaca.Clear();
            txtChassi.Clear();
            cmbMarcas.SelectedIndex = -1;  // Limpa a seleção do ComboBox de marcas
            txtAnoFabricacao.Clear();
            txtAnoModelo.Clear();
            txtValorFipe.Clear();
            txtValorVenda.Clear();
            txtObservacoes.Clear();
        }

        public async Task ObterMarcas()
        {
            try
            {
                string apiUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var marcas = JsonSerializer.Deserialize<List<Marcas>>(responseData);

                    cmbMarcas.DataSource = marcas;
                    cmbMarcas.DisplayMember = "Nome";  // O que será mostrado no ComboBox
                    cmbMarcas.ValueMember = "Codigo";  // O valor associado ao item
                }
                else
                {
                    MessageBox.Show("Erro ao carregar as marcas: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as marcas: " + ex.Message);
            }
        }

        private void cmbMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica para quando uma marca for selecionada, se necessário
        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChassi_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAnoFabricacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAnoModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtValorFipe_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtObservacoes_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvVeiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtValorVenda_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
