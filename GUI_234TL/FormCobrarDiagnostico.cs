using Servicios_234TL.Observer_234TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCobrarDiagnostico : Form, IObserver_234TL<Dictionary<string, string>>
    {
        public FormCobrarDiagnostico()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            throw new NotImplementedException();
        }

        private void FormCobrarDiagnostico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}
