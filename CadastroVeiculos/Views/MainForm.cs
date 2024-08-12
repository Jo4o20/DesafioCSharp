using CadastroVeiculos.Data;
using CadastroVeiculos.Models;
using CadastroVeiculos.Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroVeiculos.Views
{
    public partial class MainForm : Form
    {

        MySqlConnection Conexao;

        public MainForm()
        {
            InitializeComponent();

            // Adicionando os eventos de click no formulario
            this.dgvVeiculos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVeiculos_CellClick);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Carrega os dados dos veículos no DataGridView ao carregar o formulário
            CarregarVeiculos();
        }

        private void CarregarVeiculos()
        {
            try
            {
                // Usando a classe ConexaoBD para obter a conexão
                MySqlConnection conexao = ConexaoBD.GetConnection();

                if (conexao != null)
                {
                    // Criação do comando SQL para seleção
                    string query = "SELECT * FROM Veiculos";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    // Preenche o DataTable com os dados
                    da.Fill(dt);

                    // Define o DataSource do DataGridView como o DataTable preenchido
                    dgvVeiculos.DataSource = dt;

                    // Fecha a conexão após o uso
                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private Veiculo ObterVeiculo()
        {
            Veiculo veiculo = new Veiculo();

            veiculo.Placa = txtPlaca.Text;
            veiculo.Chassi = txtChassi.Text;
            veiculo.Marca = txtMarca.Text;
            veiculo.Modelo = txtModelo.Text;
            veiculo.AnoFabricacao = int.Parse(txtAnoFabricacao.Text);
            veiculo.AnoModelo = int.Parse(txtAnoModelo.Text);
            veiculo.ValorFipe = decimal.Parse(txtValorFipe.Text);
            veiculo.ValorVenda = decimal.Parse(txtValorVenda.Text);
            veiculo.Observacoes = txtObservacoes.Text;

            return veiculo;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChassi_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAnoFabricacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAnoModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtValorFipe_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtValorVenda_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtObservacoes_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Usando a classe ConexaoBD para obter a conexão
                MySqlConnection conexao = ConexaoBD.GetConnection();

                if (conexao != null)
                {
                    // Criação do comando SQL para inserção
                    string query = "INSERT INTO Veiculos (Placa, Chassi, Marca, Modelo, AnoFabricacao, AnoModelo, ValorFipe, ValorVenda, Observacoes) " +
                                   "VALUES (@Placa, @Chassi, @Marca, @Modelo, @AnoFabricacao, @AnoModelo, @ValorFipe, @ValorVenda, @Observacoes)";

                    Veiculo veiculo = ObterVeiculo();

                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("@Placa", veiculo.Placa);
                    cmd.Parameters.AddWithValue("@Chassi", veiculo.Chassi);
                    cmd.Parameters.AddWithValue("@Marca", veiculo.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                    cmd.Parameters.AddWithValue("@AnoFabricacao", veiculo.AnoFabricacao);
                    cmd.Parameters.AddWithValue("@AnoModelo", veiculo.AnoModelo);
                    cmd.Parameters.AddWithValue("@ValorFipe", veiculo.ValorFipe);
                    cmd.Parameters.AddWithValue("@ValorVenda", veiculo.ValorVenda);
                    cmd.Parameters.AddWithValue("@Observacoes", veiculo.Observacoes);


                    // Executa o comando
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Veículo adicionado com sucesso!");

                    CarregarVeiculos();

                    // Fecha a conexão após o uso
                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Limpa os campos de texto
            txtObservacoes.Text = string.Empty;
            txtValorVenda.Text = string.Empty;
            txtValorFipe.Text = string.Empty;
            txtAnoModelo.Text = string.Empty;
            txtAnoFabricacao.Text = string.Empty;
            txtChassi.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtMarca.Text = string.Empty;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVeiculos.SelectedRows.Count > 0)
                {
                    // Obtém o ID do veículo selecionado
                    int id = Convert.ToInt32(dgvVeiculos.SelectedRows[0].Cells["Id"].Value);

                    // Usando a classe ConexaoBD para obter a conexão
                    MySqlConnection conexao = ConexaoBD.GetConnection();

                    if (conexao != null)
                    {
                        // Criação do comando SQL para atualização
                        string query = "UPDATE Veiculos SET Placa = @Placa, Chassi = @Chassi, Marca = @Marca, Modelo = @Modelo, " +
                                       "AnoFabricacao = @AnoFabricacao, AnoModelo = @AnoModelo, ValorFipe = @ValorFipe, ValorVenda = @ValorVenda, Observacoes = @Observacoes " +
                                       "WHERE Id = @Id";

                        Veiculo veiculo = ObterVeiculo();

                        MySqlCommand cmd = new MySqlCommand(query, conexao);

                        cmd.Parameters.AddWithValue("@Placa", veiculo.Placa);
                        cmd.Parameters.AddWithValue("@Chassi", veiculo.Chassi);
                        cmd.Parameters.AddWithValue("@Marca", veiculo.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                        cmd.Parameters.AddWithValue("@AnoFabricacao", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@AnoModelo", veiculo.AnoModelo);
                        cmd.Parameters.AddWithValue("@ValorFipe", veiculo.ValorFipe);
                        cmd.Parameters.AddWithValue("@ValorVenda", veiculo.ValorVenda);
                        cmd.Parameters.AddWithValue("@Observacoes", veiculo.Observacoes);

                        // Executa o comando
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Veículo atualizado com sucesso!");

                        // Atualiza a exibição no DataGridView
                        CarregarVeiculos();

                        // Fecha a conexão após o uso
                        conexao.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um veículo para editar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVeiculos.SelectedRows.Count > 0)
                {
                    // Obtém o ID do veículo selecionado
                    int id = Convert.ToInt32(dgvVeiculos.SelectedRows[0].Cells["Id"].Value);

                    // Confirmação antes de excluir
                    DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este veículo?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Usando a classe ConexaoBD para obter a conexão
                        MySqlConnection conexao = ConexaoBD.GetConnection();

                        if (conexao != null)
                        {
                            // Criação do comando SQL para exclusão
                            string query = "DELETE FROM Veiculos WHERE Id = @Id";

                            MySqlCommand cmd = new MySqlCommand(query, conexao);
                            cmd.Parameters.AddWithValue("@Id", id);

                            // Executa o comando
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Veículo excluído com sucesso!");

                            // Atualiza a exibição no DataGridView
                            CarregarVeiculos();

                            // Fecha a conexão após o uso
                            conexao.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um veículo para excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void dgvVeiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Certifique-se de que a linha clicada é válida
            {
                DataGridViewRow row = dgvVeiculos.Rows[e.RowIndex];

                txtPlaca.Text = row.Cells["Placa"].Value.ToString();
                txtChassi.Text = row.Cells["Chassi"].Value.ToString();
                txtMarca.Text = row.Cells["Marca"].Value.ToString();
                txtModelo.Text = row.Cells["Modelo"].Value.ToString();
                txtAnoFabricacao.Text = row.Cells["AnoFabricacao"].Value.ToString();
                txtAnoModelo.Text = row.Cells["AnoModelo"].Value.ToString();
                txtValorFipe.Text = row.Cells["ValorFipe"].Value.ToString();
                txtValorVenda.Text = row.Cells["ValorVenda"].Value.ToString();
                txtObservacoes.Text = row.Cells["Observacoes"].Value.ToString();
            }
        }

    }
}
