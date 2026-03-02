using System;
using System.IO;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Printing.IndexedProperties;
using System.Printing;

namespace Easy_Tags
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string TargetPrinter;
        public MainWindow()
        {
            InitializeComponent();
            TargetPrinter = GetDefaultPrintQueue();
            PrinterName.Content = TargetPrinter;
        }

        private string GetDefaultPrintQueue()
        {
            LocalPrintServer lpServer = new LocalPrintServer();
            PrintQueue printQueue = lpServer.DefaultPrintQueue;
            return printQueue.FullName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int startTag = 0, endTag = 0;
            string job, counts, stream, description;
            if (JobNumber.Text == String.Empty) job = "--";
            else job = JobNumber.Text;
            if (Description.Text == String.Empty) description = "--";
            else description = Description.Text;
            if (Int32.TryParse(StartTag.Text, out startTag) && Int32.TryParse(EndTag.Text, out endTag))
            {
                if(description.Length <= 40)
                {
                    if ((bool)ReversePrint.IsChecked)
                    {
                        if (TargetPrinter.Contains("ZDesigner"))
                        {
                            for (int i = endTag; i >= startTag; i--)
                            {
                                counts = ((bool)IncludeEnd.IsChecked) ? String.Format("{0}/{1}", i.ToString("D3"), endTag.ToString("D3")) : i.ToString("D3");
                                stream = ZebraPrinterHelper.GetTrayTag(counts, job, description);
                                if (!ZebraPrinterHelper.SendStringToPrinter(TargetPrinter, stream))
                                {
                                    MessageBox.Show("Error communicating with printer.");
                                    break;
                                }
                            }
                            stream = ZebraPrinterHelper.GetBreakTag();
                            ZebraPrinterHelper.SendStringToPrinter(TargetPrinter, stream);
                        }
                        else
                        {
                            for (int i = endTag; i >= startTag; i--)
                            {
                                counts = ((bool)IncludeEnd.IsChecked) ? String.Format("{0}/{1}", i.ToString("D3"), endTag.ToString("D3")) : i.ToString("D3");
                                stream = RawPrinterHelper.GetTrayTag(counts, job, description);
                                if (!RawPrinterHelper.SendStringToPrinter(TargetPrinter, stream))
                                {
                                    MessageBox.Show("Error communicating with printer.");
                                    break;
                                }
                            }
                            stream = RawPrinterHelper.GetBreakTag();
                            RawPrinterHelper.SendStringToPrinter(TargetPrinter, stream);
                        }
                    }
                    else
                    {
                        if (TargetPrinter.Contains("ZDesigner"))
                        {
                            for(int i = startTag; i <= endTag; i++)
                            {
                                counts = ((bool)IncludeEnd.IsChecked) ? String.Format("{0}/{1}", i.ToString("D3"), endTag.ToString("D3")) : i.ToString("D3");
                                stream = ZebraPrinterHelper.GetTrayTag(counts, job, description);
                                if(!ZebraPrinterHelper.SendStringToPrinter(TargetPrinter, stream))
                                {
                                    MessageBox.Show("Error communicating with printer.");
                                    break;
                                }
                            }
                            stream = ZebraPrinterHelper.GetBreakTag();
                            ZebraPrinterHelper.SendStringToPrinter(TargetPrinter, stream);
                        }
                        else
                        {
                            for(int i = startTag; i <= endTag; i++)
                            {
                                counts = ((bool)IncludeEnd.IsChecked) ? String.Format("{0}/{1}", i.ToString("D3"), endTag.ToString("D3")) : i.ToString("D3");
                                stream = RawPrinterHelper.GetTrayTag(counts, job, description);
                                if (!RawPrinterHelper.SendStringToPrinter(TargetPrinter, stream))
                                {
                                    MessageBox.Show("Error communicating with printer.");
                                    break;
                                }
                            }
                            stream = RawPrinterHelper.GetBreakTag();
                            RawPrinterHelper.SendStringToPrinter(TargetPrinter, stream);
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Description must be 40 or fewer characters.");
                }
            }
            else
            {
                MessageBox.Show("Tag numbers must be valid integers.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            bool? print = printDialog.ShowDialog();
            if(print == true)
            {
                TargetPrinter = printDialog.PrintQueue.FullName;
                PrinterName.Content = TargetPrinter;
            }
        }
    }
}
