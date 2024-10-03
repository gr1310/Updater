using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

using DataExchangeModule;

namespace ViewModels

{
    public class LoadClientInfo
    {
        private IList<Tool> _ClientInfoList { get; set; }

        public LoadClientInfo()
        {
            DllLoader dllLoader = new DllLoader();
            Dictionary<string, List<string>> hashMap = dllLoader.LoadToolsFromFolder(@"C:\temp");
            if (hashMap.Count > 0)
            {
                int rowCount = hashMap.Values.First().Count;
                _ClientInfoList = new List<Tool>();



                for (int i = 0; i < rowCount; i++)
                {
                    _ClientInfoList.Add(new Tool { ID = hashMap["Id"][i], Version = hashMap["Version"][i], Description = hashMap["Description"][i], Deprecated = hashMap["IsDeprecated"][i], CreatedBy = hashMap["CreatorName"][i] });
                }
            }
        }

        public IList<Tool> ClientInfo
        {
            get { return _ClientInfoList; }
            set { _ClientInfoList = value; }
        }

        private ICommand mUpdater;

        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                // Code implementation for execution
            }
        }
    }
}
