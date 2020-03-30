using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_Principal : Form
    {
        #region Atributos e Propriedades

        /// <summary>
        /// Classe que apresenta a conexão
        /// </summary>
        Util.JSON.JS_Conexao conexao = null;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento acionado no clique do botão de informação do servidor SQLIte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_infoDatabaseSLIte_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Banco a ser conectado a ser conectado!", "Informação");
        }

        /// <summary>
        /// Evento acionado no clique do botão de informação do servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_servidorSqlServer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Servidor a ser conectado!", "Informação");
        }

        /// <summary>
        /// Evento acionado no clique do botão de informação da DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_DataBaseSqlServer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Database a ser conectado!", "Informação");
        }

        /// <summary>
        /// Evento acionado no clique do botão de informação do Usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_Usuario_SqlServer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuário do banco de dados!", "Informação");
        }

        /// <summary>
        /// Evento acionado no clique do botão de informação da senha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_SenhaSqlServer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Senha do banco de dados!", "Informação");
        }

        /// <summary>
        /// Evento lançado no botão importar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            this.ConfirmarSQLServer();
        }

        /// <summary>
        /// Evento lançado no botão importar SQLIte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_importarSQLIte_Click(object sender, EventArgs e)
        {
            this.ConfirmaSQLite();
        }

        /// <summary>
        /// Evento lançado no clque do botão de seleção de arquivos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog_f = new OpenFileDialog();
            dialog_f.Title = "Seleção do banco para alteração!";

            if (dialog_f.ShowDialog() == DialogResult.OK)
            {
                this.tbx_dataBaseSQLIte.Text = dialog_f.FileName.ToString();
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public FO_Principal()
        {
            this.InitializeComponent();
            this.InicializaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        private void InicializaForm()
        {
            this.conexao = Util.JSON.Geral.InstanciaConexao();
            if (Util.Enumerator.BancoDeDadosPorCodigo(conexao.TipoConexao) == Util.Enumerator.BancoDados.SQL_SERVER)
            {
                this.tbx_dataBaseSqlServer.Text = this.conexao.DataBase;
                this.tbx_senhaSqlServer.Text = this.conexao.Senha;
                this.tbx_servidorSqlServer.Text = this.conexao.Servidor;
                this.tbx_usuarioSQLServer.Text = this.conexao.Usuario;
            }
            else if(Util.Enumerator.BancoDeDadosPorCodigo(this.conexao.TipoConexao) == Util.Enumerator.BancoDados.SQLite)
            {
                this.tbx_dataBaseSQLIte.Text = this.conexao.DataBase;
            }
        }

        /// <summary>
        /// Método que valida os campos do formulário
        /// </summary>
        /// <returns>True - validado; false - errado</returns>
        private bool ValidaCamposSQLServer()
        {
            bool retorno = true;

            if (string.IsNullOrEmpty(this.tbx_servidorSqlServer.Text))
            {
                retorno = false;
                MessageBox.Show("Campo servidor está vazio!", "Alerta");
                this.tbx_servidorSqlServer.Focus();
            }
            else if (string.IsNullOrEmpty(this.tbx_dataBaseSqlServer.Text))
            {
                retorno = false;
                MessageBox.Show("Campo DataBase está vazio!", "Alerta");
                this.tbx_dataBaseSqlServer.Focus();
            }
            else if (string.IsNullOrEmpty(this.tbx_usuarioSQLServer.Text))
            {
                retorno = false;
                MessageBox.Show("Campo usuário está vazio!", "Alerta");
                this.tbx_usuarioSQLServer.Focus();
            }
            else if (string.IsNullOrEmpty(this.tbx_senhaSqlServer.Text))
            {
                retorno = false;
                MessageBox.Show("Campo senha está vazio!", "Alerta");
                this.tbx_senhaSqlServer.Focus();
            }

            return retorno;
        }

        /// <summary>
        /// Método que confirma o formulário de configuração do sql server
        /// </summary>
        private void ConfirmarSQLServer()
        {
            if (ValidaCamposSQLServer())
            {
                this.conexao.TipoConexao = (int)Util.Enumerator.BancoDados.SQL_SERVER;
                this.conexao.DataBase = this.tbx_dataBaseSqlServer.Text;
                this.conexao.Senha = this.tbx_senhaSqlServer.Text;
                this.conexao.Servidor = this.tbx_servidorSqlServer.Text;
                this.conexao.Usuario = this.tbx_usuarioSQLServer.Text;

                MessageBox.Show("Configuração realizada com sucesso!", "Sucesso");
            }
        }

        /// <summary>
        /// Método que valida os campos do formulário
        /// </summary>
        /// <returns>True - validado; false - errado</returns>
        private bool ValidaCamposSQLite()
        {
            bool retorno = true;

            if (string.IsNullOrEmpty(this.tbx_dataBaseSQLIte.Text))
            {
                retorno = false;
                MessageBox.Show("Campo database está vazio!", "Alerta");
                this.tbx_servidorSqlServer.Focus();
            }

            return retorno;
        }

        /// <summary>
        /// Método que confirma o formulário do sqlite
        /// </summary>
        private void ConfirmaSQLite()
        {
            if (ValidaCamposSQLite())
            {
                this.conexao.DataBase = this.tbx_dataBaseSQLIte.Text;
                this.conexao.TipoConexao = (int)Util.Enumerator.BancoDados.SQLite;
                Util.JSON.Geral.SalvaArquivo(this.conexao, Util.Global.app_File_config_BD);
                MessageBox.Show("Configuração realizada com sucesso!", "Sucesso");
            }
        }

        #endregion Métodos
    }
}
