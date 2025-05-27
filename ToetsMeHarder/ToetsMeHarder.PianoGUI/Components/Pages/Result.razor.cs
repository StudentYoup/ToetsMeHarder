using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business;
using ToetsMeHarder.DataAccess;

namespace ToetsMeHarder.PianoGUI.Components.Pages
{
    public partial class Result : ComponentBase
    {
        
        private Business.Result result;


        private DataManager dataManager { get; set; }
        
        
        protected override void OnInitialized()
        {
            dataManager = new DataManager();
            dataManager.Connect();
            dataManager.GetResult(1);
            result = dataManager.GetResult(1);
            base.OnInitialized();
        }

        
    }
}
