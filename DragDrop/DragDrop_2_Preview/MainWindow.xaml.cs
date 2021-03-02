using System;
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

namespace DragDrops
{
  /// <summary>
  /// MainWindow.xaml 的互動邏輯
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Button_MouseMove(object sender, MouseEventArgs e)
    {
      if (Mouse.LeftButton == MouseButtonState.Pressed)
      {
        DataObject data = new DataObject();
        data.SetData(DataFormats.StringFormat, "我是資料");
        data.SetData(typeof(Label), sender);

        DragDrop.DoDragDrop(sender as DependencyObject, data, DragDropEffects.Move);
      }
    }

    private void StackPanel_Drop(object sender, DragEventArgs e)
    {
      var st = sender as StackPanel;

      var lbl = e.Data.GetData(typeof(Label)) as Label;
      var oriLblParent = VisualTreeHelper.GetParent(lbl) as Panel;
      if (oriLblParent != null)
      {
        oriLblParent.Children.Remove(lbl);
        st.Children.Add(lbl);
        e.Effects = DragDropEffects.Move;
      }


    }

    private void Label_GiveFeedback(object sender, GiveFeedbackEventArgs e)
    {
      if (e.Effects.HasFlag(DragDropEffects.Copy))
      {
        Mouse.SetCursor(Cursors.Cross);
      }
      else if (e.Effects.HasFlag(DragDropEffects.Move))
      {
        Mouse.SetCursor(Cursors.Pen);
      }
      else
      {
        Mouse.SetCursor(Cursors.No);
      }
      e.Handled = true;
    }

    private void StackPanel_DragOver(object sender, DragEventArgs e)
    {
      e.Effects = DragDropEffects.None;
      if (e.Data.GetDataPresent(DataFormats.StringFormat))
      {
        //有指定的資料
        var strContent =(string) e.Data.GetData(DataFormats.StringFormat);
        e.Effects = DragDropEffects.Move;

      }
      e.Handled = true;


    }
  }
}
