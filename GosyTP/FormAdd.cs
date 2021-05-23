using GosyTP.ViewModels;
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
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
        }

        private CarViewModel model;

        public CarViewModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
                textBoxName.Text = model.Name;
                textBoxModel.Text = model.Model;
                textBoxPower.Text = model.Power.ToString();
            }
        }

        [Dependency]
        public new IUnityContainer Container { get; set; }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните поле имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxModel.Text))
            {
                MessageBox.Show("Заполните поле модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPower.Text))
            {
                MessageBox.Show("Заполните поле мощность", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new CarViewModel
                    {
                        Name = textBoxName.Text,
                        Model = textBoxModel.Text,
                        Power = Convert.ToInt32(textBoxPower.Text)
                    };
                }
                else
                {
                    model.Name = textBoxName.Text;
                    model.Model = textBoxModel.Text;
                    model.Power = Convert.ToInt32(textBoxPower.Text);
                }
                
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
