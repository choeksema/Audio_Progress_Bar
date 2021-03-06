using System.Windows.Forms;
using System;
using System.Drawing;


namespace System.Windows.Forms
{
	public partial class AudioBar : UserControl
	{
		int min = 0;// Minimum value for progress range
		int max = 100;// Maximum value for progress range
		int val = 0;// Current progress
		Color BarColor = Color.Blue;// Color of progress meter
		
		public AudioBar()
		{
			InitializeComponent();
		}
		
		protected override void OnResize(EventArgs e)
		{
		    // Invalidate the control to get a repaint.
		    this.Invalidate();
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
		    Graphics g = e.Graphics;
		    SolidBrush brush = new SolidBrush(BarColor);
		    float percent = (float)(val - min) / (float)(max - min);
		    Rectangle rect = this.ClientRectangle;
		
		    // Calculate area for drawing the progress.
		    rect.Width = (int)((float)rect.Width * percent);
		
		    // Draw the progress meter.
		    g.FillRectangle(brush, rect);
		
		    // Clean up.
		    brush.Dispose();
		    g.Dispose();
		}
		
		public new int Minimum
		{
		    get
		    {
		        return min;
		    }
		
		    set
		    {
		        // Prevent a negative value.
		        if (value < 0)
		        {
		            min = 0;
		        }
		
		        // Make sure that the minimum value is never set higher than the maximum value.
		        if (value > max)
		        {
		            min = value;
		            min = value;
		        }
		
		        // Ensure value is still in range
		        if (val < min)
		        {
		            val = min;
		        }
		
		        // Invalidate the control to get a repaint.
		        this.Invalidate();
		    }
		}
		
		public new int Maximum
		{
		    get
		    {
		        return max;
		    }
		
		    set
		    {
		        // Make sure that the maximum value is never set lower than the minimum value.
		        if (value < min)
		        {
		            min = value;
		        }
		
		        max = value;
		
		        // Make sure that value is still in range.
		        if (val > max)
		        {
		            val = max;
		        }
		
		        // Invalidate the control to get a repaint.
		        this.Invalidate();
		    }
		}
		
		public new  int Value
		{
		    get
		    {
		        return val;
		    }
		
		    set
		    {
		        int oldValue = val;
		
		        // Make sure that the value does not stray outside the valid range.
		        if (value < min)
		        {
		            val = min;
		        }
		        else if (value > max)
		        {
		            val = max;
		        }
		        else
		        {
		            val = value;
		        }
		        
		        // Invalidate only the changed area.
		        float percent;
		
		        Rectangle newValueRect = this.ClientRectangle;
		        Rectangle oldValueRect = this.ClientRectangle;
		
		        // Use a new value to calculate the rectangle for progress.
		        percent = (float)(val - min) / (float)(max - min);
		        newValueRect.Width = (int)((float)newValueRect.Width * percent);
		
		        // Use an old value to calculate the rectangle for progress.
		        percent = (float)(oldValue - min) / (float)(max - min);
		        oldValueRect.Width = (int)((float)oldValueRect.Width * percent);
		
		        Rectangle updateRect = new Rectangle();
		
		        // Find only the part of the screen that must be updated.
		        if (newValueRect.Width > oldValueRect.Width)
		        {
		            updateRect.X = oldValueRect.Size.Width;
		            updateRect.Width = newValueRect.Width - oldValueRect.Width;
		        }
		        else
		        {
		            updateRect.X = newValueRect.Size.Width;
		            updateRect.Width = oldValueRect.Width - newValueRect.Width;
		        }
		
		        updateRect.Height = this.Height;
		
		        // Invalidate the intersection region only.
		        this.Invalidate(updateRect);
		        // this.Refresh();
		        // this.Parent.Invalidate(this.Parent.DisplayRectangle);
		    }
		}
		
		public Color ProgressBarColor
		{
		    get
		    {
		        return BarColor;
		    }
		
		    set
		    {
		        BarColor = value;
		
		        // Invalidate the control to get a repaint.
		        this.Invalidate();
		    }
		}
	}
}
