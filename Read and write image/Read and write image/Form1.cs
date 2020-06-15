using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_and_write_image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int BadCode;
            int x, y, z;
            int x_counter_L, x_L;
            int z1  , No_of_Images ;
            int Row;
            //int frame = 0, Movie_Length = 10;// 10 frames
            int x_tempo, y_tempo, shift_R, shift_L, C;
            int a, r, g, b;
            int x_Fun, y_Fun;
            int x_rec, y_rec, y_rec1;
            int Right_Image_Rectification_Factor, Left_Image_Rectification_Factor;
            int Cut_from_uper_Right, Cut_from_uper_Left;
            int No_of_Rows_Right, No_of_Rows_Left;
            int x1, x2, y1, y2;
            int X1_R ,Y1_R , X2_R , Y2_R , X1_L,Y1_L, X2_L, Y2_L;
            double x_temp, y_temp, y_temp1, oper1, oper2, oper3, oper4, oper5, oper6, m_R, m_L;
            int calctemp;
            double slope_R, slope_L;
            // double slope_R = 0.00028, slope_L = 0.00028;
            int x_shift_L, y_shift_L, x_shift_R, y_shift_R;
            int widthFun_R, heightFun_R, widthFun_L, heightFun_L, width_Stetch, height_Stetch;
            int width_R, height_R, width_L, height_L;


            int Rectification_Factor_Horizontal_Step_1 = 270, Rectification_Factor_Horizontal_Step_2 = 1, Rectification_Factor_vertical = 1;
            int Scale_Factor_H = 1;
            int Scale_Factor_W = 1;
            int Trim_C_From_Right = 600, Trim_C_From_Left = 0, Trim_R_From_Left = 0, Trim_L_From_Right = 7;

            string Folder_Path = @"H:\Image Processing\C# Trials\Pixel Level\Images Test\Output\";
            string File_Name = "Stetched_Img-";
            string File_Name_counter = "2";
            string check;

            string Folder_Path_R = @"H:\Image Processing\C# Trials\Pixel Level\Images Test\Input\Camera 2 input\";
            string File_Name_counT_R;
            string Folder_Path_L = @"H:\Image Processing\C# Trials\Pixel Level\Images Test\Input\Camera 1 input\";
            string File_Name_counT_L;
            string finename_R = "Canera2-";
            string finename_L = "Canera1-";
            //string Folder_Path = Folder_Path + File_Name;

            double d = 0.142857143;

            slope_R = 0.0007;// 0; //0.0007
            slope_L = 0;// -0.00028;// 0.00040;
            Right_Image_Rectification_Factor = 30;       Left_Image_Rectification_Factor = 23;

    for (BadCode = 0; BadCode < 265; BadCode++) // to reduce matrix dimension
            {
                z1 = BadCode + 637;
                No_of_Images = 1;
                for (z = 0; z < No_of_Images; z++)
                {

                    File_Name_counT_R = finename_R + z1.ToString("0000") + ".Jpeg";
                    File_Name_counT_L = finename_L + z1.ToString("0000") + ".Jpeg";
                    check = Folder_Path_R + File_Name_counT_R;
                    //z1++;

                    //read image
                    Bitmap Img_R = new Bitmap(Folder_Path_R + File_Name_counT_R);
                    Bitmap Img_L = new Bitmap(Folder_Path_L + File_Name_counT_L);

                    width_R = Img_R.Width;  // Wadi Degla 640
                    height_R = Img_R.Height;// Wadi Degla 480
                    width_L = Img_L.Width;  // Wadi Degla 640
                    height_L = Img_L.Height;// Wadi Degla 480

                    widthFun_R = width_R * Scale_Factor_W;
                    heightFun_R = height_R * Scale_Factor_H;
                    widthFun_L = width_R * Scale_Factor_W;
                    heightFun_L = height_R * Scale_Factor_H;
                    width_Stetch = width_R + width_L;// + 1000;
                    height_Stetch = height_R + Right_Image_Rectification_Factor + 10;

                    X1_R = 06; Y2_R = 0;
                    X2_R = (width_R * 3 / 4); Y1_R = Y2_R + Right_Image_Rectification_Factor;
                    X1_L = 80; Y1_L = 0;
                    X2_L = width_L - 05; Y2_L = Y1_L + Left_Image_Rectification_Factor;
                    No_of_Rows_Right = 200; No_of_Rows_Left = 200;
                    Cut_from_uper_Right = 260; Cut_from_uper_Left = Cut_from_uper_Right - 30;//213


                    Bitmap bmp_Fun_R = new Bitmap(widthFun_R, heightFun_R);
                    Bitmap bmp_Fun_L = new Bitmap(widthFun_L, heightFun_L);
                    Bitmap Img_Stetch = new Bitmap(width_Stetch, height_Stetch);

                    int[,,] Red_R = new int[width_R, height_R, No_of_Images];
                    int[,,] Grn_R = new int[width_R, height_R, No_of_Images];
                    int[,,] Blu_R = new int[width_R, height_R, No_of_Images];
                    int[,,] Red_L = new int[width_L, height_L, No_of_Images];
                    int[,,] Grn_L = new int[width_L, height_L, No_of_Images];
                    int[,,] Blu_L = new int[width_L, height_L, No_of_Images];

                    int[,] Red_Fun_R = new int[widthFun_R, heightFun_R];
                    int[,] Grn_Fun_R = new int[widthFun_R, heightFun_R];
                    int[,] Blu_Fun_R = new int[widthFun_R, heightFun_R];
                    int[,] Red_Fun_L = new int[widthFun_L, heightFun_L];
                    int[,] Grn_Fun_L = new int[widthFun_L, heightFun_L];
                    int[,] Blu_Fun_L = new int[widthFun_L, heightFun_L];

                    int[,,] Red_Stetch = new int[width_Stetch, height_Stetch, No_of_Images];
                    int[,,] Grn_Stetch = new int[width_Stetch, height_Stetch, No_of_Images];
                    int[,,] Blu_Stetch = new int[width_Stetch, height_Stetch, No_of_Images];

                    //Right Picture pixel matrix filling
                    for (y = 0; y < height_R; y++)
                    {
                        for (x = 0; x < width_R; x++)
                        {
                            // get pixel value
                            Color p = Img_R.GetPixel(x, y);

                            // extract ARGB value from p
                            a = p.A;
                            r = p.R;
                            g = p.G;
                            b = p.B;

                            Red_R[x, y, z] = r;
                            Grn_R[x, y, z] = g;
                            Blu_R[x, y, z] = b;

                            // set new ARGB Value in pixel
                            //bmpMan.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                        }
                    }
                    //Left Picture pixel matrix filling
                    for (y = 0; y < height_L; y++)
                    {
                        for (x = 0; x < width_L; x++)
                        {
                            // get pixel value
                            Color p = Img_L.GetPixel(x, y);

                            // extract ARGB value from p
                            a = p.A;
                            r = p.R;
                            g = p.G;
                            b = p.B;

                            Red_L[x, y, z] = r;
                            Grn_L[x, y, z] = g;
                            Blu_L[x, y, z] = b;

                            // set new ARGB Value in pixel
                            //bmpMan.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                        }
                    }


                    x = 0;
                    y = 0;
                    //Center Picture pixel matrix filling // Deleted AMA Sep 2017

                    x = 0;
                    y = 0;
                    //initiation for linear interpolationin Vertical diriction
                    x1 = 0;
                    x2 = Rectification_Factor_vertical;
                    y1 = height_R;
                    y2 = 0;
                    y_rec = 0;
                    x = 0;
                    y = 0;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //initiat lines for Right image
                    /* x1 = X1_R;// 50;//width_R;
                     y1 = Y1_R;// 50;//0;
                     x2 = X2_R; // 780;// width_R - 0;//0;
                     y2 = Y2_R; // 0;//Rectification_Factor_Horizontal_Step_1;
                     shift = Cut_from_uper_Right;
                     oper1 = y2 - y1;
                     oper2 = x2 - x1;
                     m = oper1 / oper2;*/
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //Draw lines for right image
                    /* for (Row = 0; Row < No_of_Rows_Right; Row++)
                     {
                         for (y = 0; y < height_R; y++)
                         {
                             for (x = x1; x < x2; x++)
                             {
                                 x_rec = x;
                                 C = shift + Row;
                                 y_rec = C;
                                //    y_rec = y;// test to sritch original images
                                 y_temp = m * x + (C);
                                 y_tempo = (int)Math.Truncate(y_temp);

                                 if (y == y_tempo && x_rec < width_R && y_rec < height_R)  // sep 2017      if (y == y_tempo)
                                 {
                                     Red_Fun_R[x_rec, y_rec] = Red_R[x, y, z];
                                     Grn_Fun_R[x_rec, y_rec] = Grn_R[x, y, z];
                                     Blu_Fun_R[x_rec, y_rec] = Blu_R[x, y, z];

                                     r = Red_Fun_R[x_rec, y_rec];
                                     g = Grn_Fun_R[x_rec, y_rec];
                                     b = Blu_Fun_R[x_rec, y_rec];

                                     // set new ARGB Value in pixel
                                     bmp_Fun_R.SetPixel(x, y_rec, Color.FromArgb(255, r, g, b));

                                 }
                             }
                         }
                         if (x2 < width_R - 10)
                         { x2 = x2 + 3; }
                         else
                             x2 = x2 - 5;

                         m = m - slope_R;
                     }*/
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //initiat lines for Right image
                    x1 = X1_R;// 50;//width_R;
                    y1 = Y1_R;// 50;//0;
                    x2 = X2_R; // 780;// width_R - 0;//0;
                    y2 = Y2_R; // 0;//Rectification_Factor_Horizontal_Step_1;
                    shift_R = Cut_from_uper_Right;
                    oper1 = y2 - y1;
                    oper2 = x2 - x1;
                    m_R = oper1 / oper2;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //initiat lines for Left image
                    x2 = X2_L; // width_L;//width_R;
                    y2 = Y2_L; // 50;//0;
                    x1 = X1_L; // 80;//0;
                    y1 = Y1_L;//Rectification_Factor_Horizontal_Step_1;

                    shift_L = Cut_from_uper_Left;//180; // 165 new Sep 2017
                    oper1 = y2 - y1;
                    oper2 = x2 - x1;
                    m_L = oper1 / oper2;
                    //m = 1 - m;
                    //m = 50;
                    //m = (y2- y1) / (x2- x1);
                    //x_counter_L = 0;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //Draw lines for right image
                    for (Row = 0; Row < No_of_Rows_Right; Row++)
                    {
                        for (y = 0; y < height_R; y++)
                        {
                            for (x = X1_R; x < X2_R; x++)
                            {
                                ////////////////// Right///////////////////
                                x_rec = x;
                                C = shift_R + Row;
                                y_rec = C;
                                //    y_rec = y;// test to sritch original images
                                y_temp = m_R * x + (C);
                                y_tempo = (int)Math.Truncate(y_temp);

                                if (y == y_tempo && x_rec < width_R && y_rec < height_R)  // sep 2017      if (y == y_tempo)
                                {
                                    Red_Fun_R[x_rec, y_rec] = Red_R[x, y, z];
                                    Grn_Fun_R[x_rec, y_rec] = Grn_R[x, y, z];
                                    Blu_Fun_R[x_rec, y_rec] = Blu_R[x, y, z];

                                    r = Red_Fun_R[x_rec, y_rec];
                                    g = Grn_Fun_R[x_rec, y_rec];
                                    b = Blu_Fun_R[x_rec, y_rec];

                                    // set new ARGB Value in pixel
                                    bmp_Fun_R.SetPixel(x, y_rec, Color.FromArgb(255, r, g, b));
                                }
                                ////////////////////Left//////////////////////////////
                                x_counter_L = X2_L - (x - X1_R);
                                x_L =  x_counter_L;
                                x_rec = x_L;
                                C = shift_L + Row;
                                y_rec = C;
                                // y_rec = y;// test to sritch original images
                                y_temp = m_L * x + (C);
                                y_tempo = (int)Math.Truncate(y_temp);

                                if (y == y_tempo)
                                {
                                    Red_Fun_L[x_rec, y_rec] = Red_L[x_counter_L, y, z];
                                    Grn_Fun_L[x_rec, y_rec] = Grn_L[x_counter_L, y, z];
                                    Blu_Fun_L[x_rec, y_rec] = Blu_L[x_counter_L, y, z];

                                    r = Red_Fun_L[x_rec, y_rec];
                                    g = Grn_Fun_L[x_rec, y_rec];
                                    b = Blu_Fun_L[x_rec, y_rec];

                                    // set new ARGB Value in pixel
                                    bmp_Fun_L.SetPixel(x, y_rec, Color.FromArgb(255, r, g, b));
                                }
                               // x_counter_L--;
                            }
                        }
                        /////Slope Calcs Right/////////////
                        if (X2_R < width_R - 10)
                        { X2_R = X2_R + 3; }
                        else
                            X2_R = X2_R - 5;
                        m_R = m_R - slope_R;
                        /////Slope Calcs Left/////////////
                        if (X1_L > 10)
                        { X1_L = X1_L - 4; }
                        else
                            X1_L = X1_L + 5;
                        m_L = m_L + slope_L;
                    }
                    /*
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //Draw lines for Left image
                    x2 = X2_L; // width_L;//width_R;
                    y2 = Y2_L; // 50;//0;
                    x1 = X1_L; // 80;//0;
                    y1 = Y1_L;//Rectification_Factor_Horizontal_Step_1;

                    shift_L = Cut_from_uper_Left;//180; // 165 new Sep 2017
                    oper1 = y2 - y1;
                    oper2 = x2 - x1;
                    m_L = oper1 / oper2;
                    //m = 1 - m;
                    //m = 50;
                    //m = (y2- y1) / (x2- x1);
                    
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //Draw lines for Left image
                    for (Row = 0; Row < No_of_Rows_Left; Row++)
                    {
                        for (y = 0; y < height_L; y++)
                        {
                            for (x = x2; x > x1; x--)
                            //    for (x = 2; x < width_R - 2; x++)
                            {
                                x_rec = x;
                                C = shift_L + Row;
                                y_rec = C;
                                // y_rec = y;// test to sritch original images
                                y_temp = m_L * x + (C);
                                y_tempo = (int)Math.Truncate(y_temp);

                                if (y == y_tempo)
                                {
                                    Red_Fun_L[x_rec, y_rec] = Red_L[x, y, z];
                                    Grn_Fun_L[x_rec, y_rec] = Grn_L[x, y, z];
                                    Blu_Fun_L[x_rec, y_rec] = Blu_L[x, y, z];

                                    r = Red_Fun_L[x_rec, y_rec];
                                    g = Grn_Fun_L[x_rec, y_rec];
                                    b = Blu_Fun_L[x_rec, y_rec];

                                    // set new ARGB Value in pixel
                                    bmp_Fun_L.SetPixel(x, y_rec, Color.FromArgb(255, r, g, b));
                                }
                            }
                        }

                        if (x1 > 10)
                        { x1 = x1 - 4; }
                        else
                            x1 = x1 + 5;

                        m_L = m_L + slope_L;

                    }*/
                    x_shift_L = 0;
                    y_shift_L = Left_Image_Rectification_Factor;
                   // y_shift_L = 0; //test to sritch original images
                    x_shift_R = 5;
                   y_shift_R = Right_Image_Rectification_Factor - 23;
                   // x_shift_R = 0;//test to sritch original images
                   // y_shift_R = 0;//test to sritch original images
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //Stetching for sequence of images
                    for (y = 250; y < height_Stetch - 80; y++)
                    //for (y = 0; y < height_Stetch; y++) // test to sritch original images
                    {
                        for (x = 0; x < width_Stetch; x++)
                        //for (y = 250; y < height_Stetch-70 ; y++)
                        //{
                        //    for (x = 20; x < width_Stetch-20; x++)
                        {
                            x_tempo = 0;
                            if (x <= (width_L - Trim_L_From_Right) && y < height_L)
                            //if (x < width_L && y < height_L)
                            {
                                Red_Stetch[x, y, z] = Red_Fun_L[x - x_shift_L, y - y_shift_L];
                                Grn_Stetch[x, y, z] = Grn_Fun_L[x - x_shift_L, y - y_shift_L];
                                Blu_Stetch[x, y, z] = Blu_Fun_L[x - x_shift_L, y - y_shift_L];
                            }
                            //else if (x > (width_L - Trim_L_From_Right) && x < width_Stetch - (width_L) && x_tempo < (widthFun_R-2)) // added Sep 2017
                            else if (x > (width_L - Trim_L_From_Right) && (x - (width_L - Trim_L_From_Right)) < (width_Stetch - width_L - x_shift_R) && y < heightFun_R - y_shift_R)
                            //else if (x > width_L && x < width_Stetch - (width_L))
                            //else if (x > width_L && x < width_Stetch)
                            {
                                x_tempo = x - (width_L - Trim_L_From_Right);// x - (width_L);//- (width_C - Trim_C_From_Right) + 1;
                                y_tempo = y;

                                Red_Stetch[x, y, z] = Red_Fun_R[x_tempo + x_shift_R, y + y_shift_R];
                                Grn_Stetch[x, y, z] = Grn_Fun_R[x_tempo + x_shift_R, y + y_shift_R];
                                Blu_Stetch[x, y, z] = Blu_Fun_R[x_tempo + x_shift_R, y + y_shift_R];
                            }
                            /* else
                             {
                                 Red_Stetch[x , y , z] = 200;
                                 Grn_Stetch[x , y , z] = 200;
                                 Blu_Stetch[x , y , z] = 255;
                             }*/

                            r = Red_Stetch[x, y, z];
                            g = Grn_Stetch[x, y, z];
                            b = Blu_Stetch[x, y, z];

                            // set new ARGB Value in pixel
                            Img_Stetch.SetPixel(x, y, Color.FromArgb(255, r, g, b));
                        }
                    }
                    //File_Name_counter = File_Name + Convert.ToString(z1) + ".png";
                    File_Name_counter = File_Name + BadCode.ToString("0000") + ".png";
                    check = Folder_Path + File_Name_counter;
                    Img_Stetch.Save(Folder_Path + File_Name_counter);
                    z1++;
                    /*
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //load image in in picturebox2
                    pictureBox1.Image = bmp_Fun_L;//Img_C;
                                                  //pictureBox2.Image = Img_R;
                    pictureBox2.Image = bmp_Fun_R;
                    pictureBox3.Image = Img_Stetch;

                    int tempwidth = bmp_Fun_R.Width;
                    int temphieght = bmp_Fun_R.Height;

                    //write image
                    // bmp.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\7- b Stetching Manual Trial 2\\R_Rect.bmp");

                    //save Rectified image
                    //bmp_Fun_R.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\7- b Stetching Manual Trial 2\\R_Rect.jpg");
                    //bmp_Fun_L.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\7- b Stetching Manual Trial 2\\L_Rect.png");
                    //bmp_Rec.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\7- b Stetching Manual Trial 2\\R_Rect_scale.png");
                    //Img_Stetch.Save(Folder_Path);
                    //Img_Stetch.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\8- Read inage sequence and stetch\\image sequence\\Output\\Stetched.png");
                    Img_Stetch.Save("H:\\Image Processing\\C# Trials\\Pixel Level\\12- Optimized Read image sequence, Rectification  and stetch\\image sequence\\Output\\Stetched.png");
                    //Img_R.Save("E:\\Image Processing\\C# Trials\\Pixel Level\\7- b Stetching Manual Trial 2\\R_test.png");
                    */
                }// Img_No
            }
            /*

                Img_R.Dispose();
            Img_L.Dispose();
           // Img_C.Dispose();
            Img_Stetch.Dispose();

    */

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Text = "10";//String.Format("X: {0}; Y: {1}", e.X, e.Y);
        }

    }

}
