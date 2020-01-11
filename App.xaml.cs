using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string DB_NAME = "Contacts.db";
        public static string db_folder_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public const string PORT = "1521";
        public const string USER_ID = "system";
        public const string PASSWORD = "pwd123456";
        public const string HOST = "192.168.0.38";
        public const string SERVICE_NAME = "XE";
        public static string SqlError = "";


        public const string BASE_TEXTBOX_STYLE = "baseTextBoxStyle";
        public const string DISSABLED_TEXTBOX_STYLE = "dissabledTextBoxStyle";
        public const string ERROR_TEXTBLOCK_STYLE = "errorTextBlockStyle";
        public const string ERROR_BORDER_STYLE = "errorBorderStyle";
        public const string BASE_TEXTBLOCK_STYLE = "baseTextBlockStyle";
        public const string BASE_BORDER_STYLE = "baseBorderStyle";
        public const string BASE_BUTTON_STYLE = "baseButtonStyle";
        public const string SUCCESS_BUTTON_STYLE = "successButtonStyle";
    }
}
