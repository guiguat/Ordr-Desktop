﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdrDesktop
{
    public partial class FormProduto : Form
    {
        public FormProduto()
        {
            InitializeComponent();
        }

        private async void FormProduto_Load(object sender, EventArgs e)
        {
            await getProdutos();
        }
        private async Task getProdutos()
        {
            try
            {
                var produtos = await ProdutoProcessor.getProdutos();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cod");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Preço");
                dt.Columns.Add("Estoque");
                dt.Columns.Add("Tipo");
                foreach (var produto in produtos)
                {
                    dt.Rows.Add(new object[] {
                      produto.Id,
                      produto.Nome,
                      produto.Preco,
                      produto.Estoque,
                      produto.Tipo
                    });

                }
                dgvProdutos.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Nao foi possible carregar recursos, favor checar a conexao com o servidor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task createProdutos()
        {
            string tipo = cbPrato.Checked ? "prato" : "";
            string status = await ProdutoProcessor.createProduto(txbName.Text, float.Parse(numPreco.Value.ToString()), int.Parse(numEstoque.Value.ToString()), tipo);
            MessageBox.Show(status=="OK"?"Produto cadastrado com sucesso":"Erro ao cadastrar produto");
        }
        private async Task editProdutos()
        {
            string tipo = cbPrato.Checked ? "prato" : "";
            string status = await ProdutoProcessor.editProduto(int.Parse(numCodigo.Value.ToString()),txbName.Text, float.Parse(numPreco.Value.ToString()), int.Parse(numEstoque.Value.ToString()), tipo);
            MessageBox.Show(status == "OK" ? "Produto editado com sucesso" : "Erro ao editar produto");
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            await getProdutos();
            txbName.Text = "";
            numCodigo.Value = 0;
            numEstoque.Value = 0;
            numPreco.Value = 0;
            cbPrato.Checked = false;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (txbName.Text != String.Empty && numPreco.Value != 0)
            {
                try
                {
                    await createProdutos();
                    await getProdutos();
                }
                catch
                {
                    MessageBox.Show("Erro ao cadastrar produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txbName.Text = "";
                numCodigo.Value = 0;
                numEstoque.Value = 0;
                numPreco.Value = 0;
                cbPrato.Checked = false;
            }
            else
            {
                MessageBox.Show("Favor preencher os campos corretamente","Campos mal preenchidos",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvProdutos_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           numCodigo.Value = int.Parse(dgvProdutos.SelectedRows[0].Cells[0].Value.ToString());
        }

        private async void btnEdit_ClickAsync(object sender, EventArgs e)
        {
            if (txbName.Text != String.Empty && numPreco.Value != 0 && numCodigo.Value >0)
            {
                try
                {
                    await editProdutos();
                    await getProdutos();
                }
                catch
                {
                    MessageBox.Show("Erro ao editar produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txbName.Text = "";
                numCodigo.Value = 0;
                numEstoque.Value = 0;
                numPreco.Value = 0;
                cbPrato.Checked = false;
            }
            else
            {
                MessageBox.Show("Favor preencher os campos corretamente", "Campos mal preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async void btnEstoque_Click(object sender, EventArgs e)
        {
            if (numCodigo.Value>=1)
            {
                try
                {
                    string status = await ProdutoProcessor.estoqueProduto(int.Parse(numCodigo.Value.ToString()), int.Parse(numEstoque.Value.ToString()));
                    MessageBox.Show(status == "OK" ? "Produto estocado com sucesso" : "Erro ao cadastrar produto");
                    await getProdutos();
                }
                catch
                {
                    MessageBox.Show("Erro ao estocar produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txbName.Text = "";
                numCodigo.Value = 0;
                numEstoque.Value = 0;
                numPreco.Value = 0;
                cbPrato.Checked = false;
            }
            else
            {
                MessageBox.Show("Favor preencher os campos corretamente", "Campos mal preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async void btnDel_Click(object sender, EventArgs e)
        {
            if (numCodigo.Value >= 1)
            {
                try
                {
                    string status = await ProdutoProcessor.deleteProduto(int.Parse(numCodigo.Value.ToString()));
                    MessageBox.Show(status == "OK" ? "Produto removido com sucesso" : "Erro ao remover produto");
                    await getProdutos();
                }
                catch
                {
                    MessageBox.Show("Erro ao remover produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txbName.Text = "";
                numCodigo.Value = 0;
                numEstoque.Value = 0;
                numPreco.Value = 0;
                cbPrato.Checked = false;
            }
            else
            {
                MessageBox.Show("Favor selecionar item corretamente", "Campos mal preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
