using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace vezba.Factory
{
    class ApplicationDataSource
    {
        public IRepositoryFactory CreateRepositoryFactory()
        {
            if (Properties.Settings.Default.DataSource == "file")
            {
                return new FileRepositoryFactory();
            }
            else 
            {
                MessageBox.Show("Niste izabrali izvor podataka. Prebaceno na rad sa fajlovima");
                return new FileRepositoryFactory();
            }
            
        }
    }
}
