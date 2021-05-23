using GosyTP.Interfaces;
using GosyTP.Models;
using GosyTP.ViewModels;
using GosyTP.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace GosyTP
{
    public partial class Form1 : Form
    {
        private readonly ICarService service;

        [Dependency]
        public new IUnityContainer Container { get; set; }


        public Form1(ICarService service)
        {
            InitializeComponent();
            this.service = service;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                List<CarViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAdd>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    service.AddElement(new CarBindingModel
                    {
                        Name = form.Model.Name,
                        Model = form.Model.Model,
                        Power = form.Model.Power

                    });
                }
                LoadData();
            }
        }

        private void buttonCh_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormAdd>();
                form.Model = service.GetElement(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    service.UpdElement(new CarBindingModel
                    {
                        Id = form.Model.Id,
                        Name = form.Model.Name,
                        Model = form.Model.Model,
                        Power = form.Model.Power

                    });
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            try
            {
                List<CarViewModel> list = service.GetListPowerMoreThan100();
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGroup_Click(object sender, EventArgs e)
        {
            try
            {
                List<CarViewModel> list = service.GetListGroup();
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
