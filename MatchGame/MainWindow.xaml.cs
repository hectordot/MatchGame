﻿using System;
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

namespace MatchGame 
   {
   using System.Windows.Threading;
   /// <summary>
   /// Lógica de interacción para MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window 
      {
      DispatcherTimer timer = new DispatcherTimer();
      int tenthOfSecondsElapsed;
      int matchesFound;
      public MainWindow()
      {
         InitializeComponent();

         timer.Interval = TimeSpan.FromSeconds(.1);
         timer.Tick += Timer_Tick;
         SetUpGame();
      }

      private void Timer_Tick(object sender, EventArgs e) 
      {
         tenthOfSecondsElapsed++;
         timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
         if (matchesFound == 8 )
         {
            timer.Stop();
            timeTextBlock.Text = timeTextBlock.Text + "play again?";
         }

      }

      private void SetUpGame() {
         List<String> animalEmoji = new List<String>() {
            "😀", "😀",
            "😚", "😚",
            "🥰", "🥰",
            "👧", "👧",
            "👩‍🔬", "👩‍🔬",
            "🚝", "🚝",
            "💓", "💓",
            "🍗", "🍗",
         };

         Random random = new Random();
         foreach ( TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
         {
            if ( textBlock.Name != "timeTextBlock" ) 
            {
               textBlock.Visibility = Visibility.Visible;
               int index = random.Next(animalEmoji.Count);
               String nextEmoji = animalEmoji[index];
               textBlock.Text = nextEmoji;
               animalEmoji.RemoveAt(index);
            }
         }
         timer.Start();
         tenthOfSecondsElapsed = 0;
         matchesFound = 0;
      }

      TextBlock lastTextBlockClicked;
      bool findingMatch = false;

      private void TextBlock_MouseDown(Object sender, MouseButtonEventArgs e) {
         TextBlock textBlock = sender as TextBlock;
         if ( findingMatch == false ) {
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
         }
         else if ( textBlock.Text == lastTextBlockClicked.Text ) 
         {
            matchesFound++;
            textBlock.Visibility = Visibility.Hidden;
            findingMatch = false;
         }
         else {
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
         }
      }

      private void timeTextBlock_MouseDown(Object sender, MouseButtonEventArgs e) 
      {
         if (matchesFound == 8)
         {
            SetUpGame();
         }
      }
   }
}


