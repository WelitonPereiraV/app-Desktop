﻿using Businness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP
{
    //foi definida a classe como pessoa mas ela se refere a clientes frmPessoas = frmClientes
    public partial class frmPessoa : Form
    {
        public frmPessoa()
        {
            InitializeComponent();
        }

        public static frmProdutos frmProds;

        //chama o formulario 1 e atualiza
        private void Form1_Load(object sender, EventArgs e)
        {
            
            // dtgListaClientes.DataSource = Pessoa.Lista();
        }
       
        private void mskTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        
      
        private void btnGravar_Click(object sender, EventArgs e)
        {
            // caso o id seja nullo o txtId =0;
         if(string.IsNullOrEmpty(txtId.Text))
            {
                txtId.Text = "0";
            }
            var pessoa = new Pessoa() { Id= Convert.ToInt32(txtId.Text), Nome = txtNome.Text, Cpf = mskCPF.Text, Telefone = mskTelefone.Text, Email = txtEmail.Text };
            pessoa.Salvar();

            if (txtId.Text == "0")

                MessageBox.Show("Cadastro concluido com sucesso, o seu ID é: " + pessoa.Id);
            else

                MessageBox.Show("Dado atualizado");

            // limpa todos os campos
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            mskCPF.Text = string.Empty;
            mskTelefone.Text = string.Empty;
            txtEmail.Text = string.Empty;


            dtgListaClientes.DataSource = Pessoa.Lista();

    }
       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgListaClientes.DataSource = Pessoa.Lista(txtNomePesquisa.Text);
        }

        private void dtgListaClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var pessoa = (Pessoa)dtgListaClientes.Rows[e.RowIndex].DataBoundItem;

            //var id = Convert.ToInt32(dtgListaClientes.Rows[e.RowIndex].Cells[0].Value);
            //MessageBox.Show(id.ToString());
            //MessageBox.Show(pessoa.Id.ToString());

            txtId.Text = pessoa.Id.ToString();
            txtNome.Text = pessoa.Nome;
            mskCPF.Text = pessoa.Cpf;
            txtEmail.Text = pessoa.Email;
            mskTelefone.Text = pessoa.Telefone;
            tbcCadastro.SelectedTab = tabCadastro;


            var enderecos = pessoa.Enderecos;


        }

      
        // botao para excluir cadastro
        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var pessoa = (Pessoa)dtgListaClientes.Rows[dtgListaClientes.SelectedCells[0].RowIndex].DataBoundItem;

            var confirmaExcluir = MessageBox.Show("Deseja realmente excluir ID "+ pessoa.Id +":" + pessoa.Nome + "?", "Excluir", MessageBoxButtons.YesNo);

            if(confirmaExcluir == DialogResult.Yes)
            {
                Pessoa.Excluir(pessoa.Id);
                dtgListaClientes.DataSource = Pessoa.Lista(txtNomePesquisa.Text);
            }
            
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            if (frmPessoa.frmProds == null) frmPessoa.frmProds = new frmProdutos();
            frmPessoa.frmProds.Show();
        }

       
    }
}
