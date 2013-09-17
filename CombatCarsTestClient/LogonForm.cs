using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CombatCarsTestClient
{
    public partial class LogonForm : Form
    {
        private string baseURL;

        public LogonForm(string baseURL)
        {
            InitializeComponent();
            this.baseURL = baseURL;
            logonControl1.SetBaseUrl(baseURL);
            logonControl1.LogonCallback = new LogonDelegate(this.LogonCallbackFunction);
        }

        private void LogonCallbackFunction(Token t)
        {
            DeserializedToken = t.Clone();
        }

        public delegate void LogonDelegate(Token t);

        internal Token DeserializedToken { get; private set; }
    }
}
