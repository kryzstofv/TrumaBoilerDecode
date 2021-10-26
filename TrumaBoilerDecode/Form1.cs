using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrumaBoilerDecode
{
    public partial class Form1 : Form
    {
        bool bOpen;

        BoilerDataSource boilerData;

        byte[] d1 = { 0x0, 0x55, 0x3c, 0x1, 0x6, 0xb8, 0x40, 0x3, 0x1, 0x0, 0xff, 0xfb };
        byte[] d2 = { 0x0, 0x55, 0x7d, 0x1, 0x6, 0xf8, 0x1, 0x2, 0x0, 0xff, 0x5, 0x7 }; //an invalid checksum
        byte[] d3 = { 0x0, 0x55, 0x20, 0x7c, 0xab, 0xaa, 0x0, 0x12, 0xb2, 0xe0, 0x0f, 0x58 };
        byte[] d4 = { 0x0, 0x55, 0x61, 0x4f, 0x0b, 0xc8, 0x28, 0x12, 0x22, 0xf0, 0x0f, 0x1f };
        byte[] d5 = { 0x0, 0x55, 0xe2, 0x83, 0xf0, 0x10, 0xfe, 0xff, 0xff, 0xff, 0xff, 0x99 };
        byte[] d6 = { 0x0, 0x55, 0x3c, 0x7f, 0x6, 0xb2, 0x0, 0x17, 0x46, 0x40, 0x3, 0x27 };
        byte[] d7 = { 0x0, 0x55, 0x7d, 0x1, 0x6, 0xf2, 0x17, 0x46, 0x40, 0x3, 0x9, 0x5c };
        byte[] d8 = { 0, 0x55, 0x20, 0x7c, 0xab, 0xaa, 0x0, 0x12, 0xb2, 0xe0, 0x0f, 0x58 };
        byte[] d9 = { 0, 0x55, 0x61, 0x4f, 0x0b, 0xc8, 0x28, 0x12, 0x22, 0xf0, 0x0f, 0x1f };
        byte[] d10 = { 0, 0x55, 0xe2, 0x83, 0xf0, 0x10, 0xfe, 0xff, 0xff, 0xff, 0xff, 0x99 };
        byte[] d11 = { 0, 0x55, 0x3c, 0x7f, 0x6, 0xb2, 0x23, 0x17, 0x46, 0x40, 0x3, 0x4 };

        //water mode
        byte[] wm1 = { 0, 0x55, 0x20, 0x4A, 0xAB, 0xC3, 0xFA, 0x00, 0xB1, 0xE0, 0x0F, 0x89 };
        byte[] wm2 = { 0, 0x55, 0x20, 0xAA, 0x0A, 0xCD, 0xFA, 0x00, 0x01, 0xE0, 0x0F, 0x71 };
        byte[] wm3 = { 0, 0x55, 0x20, 0xAA, 0x2A, 0xD0, 0xFA, 0x00, 0x01, 0xE0, 0x0F, 0x4E };
        byte[] wm4 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xAA, 0x00, 0x12, 0x02, 0xE0, 0x0F, 0xDB };

        //power mode
        byte[] pm1 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xC3, 0x00, 0x12, 0x02, 0xE0, 0x0F, 0xC2};
        byte[] pm2 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xC3, 0x00, 0x09, 0x02, 0xE0, 0x0F, 0xCB};
        byte[] pm3 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xC3, 0xFA, 0x12, 0x03, 0xE0, 0x0F, 0xC6};
        byte[] pm4 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xC3, 0xFA, 0x09, 0x03, 0xE0, 0x0F, 0xCF};
        byte[] pm5 = { 0, 0x55, 0x20, 0xAA, 0xAA, 0xC3, 0xFA, 0x00, 0x01, 0xE0, 0x0F, 0xDA};

        //air set point
        byte[] asp1 = { 0, 0x55, 0x20, 0xA4, 0xAB, 0xC3, 0xFA, 0x00, 0xB1, 0xE0, 0x0F, 0x2F};//25degC
        byte[] asp2 = { 0, 0x55, 0x20, 0x7C, 0xAB, 0xC3, 0xFA, 0x00, 0xB1, 0xE0, 0x0F, 0x57};//21degC
        byte[] asp3 = { 0, 0x55, 0x20, 0x4A, 0xAB, 0xC3, 0xFA, 0x00, 0xB1, 0xE0, 0x0F, 0x89};//16degC

        public Form1()
        {
            InitializeComponent();

            bOpen = false;
            boilerData = new BoilerDataSource("COM2");

            boilerData.NewDisplayData += NewDisplayData;    //listen to new display data



            LINmessage l1 = new LINmessage(d1);
            LINmessage l2 = new LINmessage(d2);//actually an invalid checksum
            LINmessage l3 = new LINmessage(d3);
            LINmessage l4 = new LINmessage(d4);
            LINmessage l5 = new LINmessage(d5);
            LINmessage l6 = new LINmessage(d6);
            LINmessage l7 = new LINmessage(d7);
            LINmessage l8 = new LINmessage(d8);
            LINmessage l9 = new LINmessage(d9);
            LINmessage l10 = new LINmessage(d10);
            LINmessage l11 = new LINmessage(d11);


            TrumaDisplayCommand a1 = new TrumaDisplayCommand(asp1);
            TrumaDisplayCommand a2 = new TrumaDisplayCommand(asp2);
            TrumaDisplayCommand a3 = new TrumaDisplayCommand(asp3);

            TrumaDisplayCommand p1 = new TrumaDisplayCommand(pm1);
            TrumaDisplayCommand p2 = new TrumaDisplayCommand(pm2);
            TrumaDisplayCommand p3 = new TrumaDisplayCommand(pm3);
            TrumaDisplayCommand p4 = new TrumaDisplayCommand(pm4);
            TrumaDisplayCommand p5 = new TrumaDisplayCommand(pm5);

            TrumaDisplayCommand w1 = new TrumaDisplayCommand(wm1);
            TrumaDisplayCommand w2 = new TrumaDisplayCommand(wm2);
            TrumaDisplayCommand w3 = new TrumaDisplayCommand(wm3);
            TrumaDisplayCommand w4 = new TrumaDisplayCommand(wm4);


            //CircularBuffer<byte> buf = new CircularBuffer<byte>(512);
            //buf.Put(new byte[] { 1, 2, 3 }); 
            //buf.Put(d1);
            //buf.Put(new byte[] { 1, 2, 3 });
            //buf.Put(d2);
            //buf.Put(d3);
            //buf.Put(new byte[] { 1, 2, 3 });
            //buf.Put(d4);
            //buf.Put(d5);
            //buf.Put(d6);
            //buf.Put(d7);
            //buf.Put(d8);
            //buf.Put(d9);
            //buf.Put(d10);
            //buf.Put(d11);


            //List<LINmessage> list = boilerData.ParseReceivedData(buf);
            //boilerData.ProcessMessages(list);
            
#if false



            //DataTable table = new DataTable();
            //for (int i = 0; i < cols; i++)
            //{
            //    string name = "D" + (i + 1);
            //    table.Columns.Add(name);
            //}

            ////Multi Dimension Array into DataTable
            //for (int outerIndex = 0; outerIndex < rows; outerIndex++)
            //{
            //    DataRow newRow = table.NewRow();
            //    for (int innerIndex = 0; innerIndex < cols; innerIndex++)
            //    {
            //        newRow[innerIndex] = rawData[outerIndex, innerIndex];
            //    }
            //    table.Rows.Add(newRow);
            //}


#endif



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!bOpen)
            {
                boilerData.Open();
                bOpen = true;
            }
            else
            {
                boilerData.Close();
                bOpen = false;
            }
        }


        void NewDisplayData(object sender, DisplayDataEventArgs e)
        {
            textBoxAirTemp.Text = "" + e.data.airTempSetPointDegs + "degC";
            textBoxPowerMode.Text = e.data.powerMode.ToString();
            textBoxWaterMode.Text = e.data.waterMode.ToString();
            textBoxVentMode.Text = "" + e.data.ventPower;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CircularBuffer<byte> buf = new CircularBuffer<byte>(512);
            buf.Put(new byte[] { 1, 2, 3 });
            buf.Put(d1);
            buf.Put(new byte[] { 1, 2, 3 });
            buf.Put(d2);
            buf.Put(d3);
            buf.Put(new byte[] { 1, 2, 3 });
            buf.Put(d4);
            buf.Put(d5);
            buf.Put(d6);
            buf.Put(d7);
            buf.Put(d8);
            buf.Put(d9);
            buf.Put(d10);
            buf.Put(d11);
            buf.Put(asp1);
            buf.Put(asp2);
            buf.Put(asp3);
            buf.Put(pm1);
            buf.Put(pm2);
            buf.Put(pm3);
            buf.Put(pm4);
            buf.Put(pm5);
            buf.Put(wm1);
            buf.Put(wm2);
            buf.Put(wm3);
            buf.Put(wm4);



            List<LINmessage> list = boilerData.ParseReceivedData(buf);
            boilerData.ProcessMessages(list);
        }
    }
}
