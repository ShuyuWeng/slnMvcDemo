using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CLottoGen
    {
        public string getNumber()
        {
            Random rand = new Random();
            int count = 0;
            int[] numbers = new int[6];
             while (count<6)
            {
                int temp = rand.Next(1, 50);//產生1~50之間的亂數
                //bool isSelected = false; //判斷產生亂數是否重複
                //for (int i=0;i<numbers.Length;i++)
                //{
                //    if (numbers[i] == temp)//重複就break退出for迴圈
                //    {
                //        isSelected = true;
                //        break;
                //    }
                //}
                //if (!isSelected)//不重複就加進技術並將變量遞增count1
                //{
                //    numbers[count] = temp;
                //    count++;
                //}

                if (!is亂數是否已經存在在陣列中(temp,numbers))
                {
                    numbers[count] = temp;
                    count++;
                }
            }

             for (int i=0;i<numbers.Length;i++)//判斷排序由小到大
            {
                for (int j=0;j<numbers.Length-1;j++)
                {
                    if (numbers[j] > numbers[j+1])
                    {
                        int big = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = big;
                    }
                }
            }
            string s = "";
            foreach (int i in numbers)
                s += i.ToString()+" ";
            return s;
        }

        private bool is亂數是否已經存在在陣列中(int temp, int[] numbers)
        {
          foreach(int i in numbers)
            {
                if (i == temp)
                    return true;
            }
            return false;
        }
    }
}