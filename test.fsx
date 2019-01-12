open System.Windows.Forms
open System.Drawing
open System

let win = new Form () // make a windowform
win.ClientSize <- Size (500, 500)

// make a label to show time
let digitalTimerLabel = new Label ()
win.Controls.Add digitalTimerLabel
digitalTimerLabel.Width <- 200
digitalTimerLabel.Location <- new Point (140,300) 
digitalTimerLabel.Text <- string System.DateTime.Now // get present time and date

// make a timer and link to label
let timer = new Timer()
timer.Interval <- 1000 // create an event every 1000 millisecond
timer.Enabled <- true // activate the timer
timer.Tick.Add (fun e -> digitalTimerLabel.Text <- string System.DateTime.Now)

// ********* Translate the clock *********
let translate (d : Point) (arr : Point []) : Point [] =
    let add (d : Point) (p : Point) : Point =
        Point (d.X + p.X, d.Y + p.Y)
    Array.map (add d) arr

// ********* Rotate the clock hands *********
let rotate (theta : float) (arr : Point []) : Point [] =
        let toInt = int << round
        let rot (t : float) (p : Point) : Point =
            let (x, y) = (float p.X, float p.Y)
            let (a, b) = (x * cos t - y * sin t, x * sin t + y * cos t)
            Point (toInt a, toInt b)
        Array.map (rot theta) arr

/// ********* ClockHands *********
let myPaint (e : PaintEventArgs) : unit =
    // HourHand
    let black = new Pen (Color.Black,Width=2.0f)
    let hourHand =
    //   [bot cord]    [top cord]
        [|Point (0,0);Point (0,-45)|]
    e.Graphics.DrawLines (black, hourHand)

    // MinuteHand
    let red = new Pen (Color.Red,Width=4.0f)
    let minuteHand =
    //   [bot cord]    [top cord]
        [|Point (0,0);Point (0,-20)|]
    e.Graphics.DrawLines (red, minuteHand)

    // SecondHand
    let green = new Pen (Color.Green,Width=1.0f)
    let secondHand =
    //   [bot cord]    [top cord]
        [|Point (0,0);Point (0,-20)|]
    e.Graphics.DrawLines (green, secondHand)

    // Circle
    let circleBlack = new Pen(Color.Black,Width=4.0f)
    let circle =
        e.Graphics.DrawEllipse(circleBlack, -100.0f,-100.0f,200.0f,200.0f)
    circle

    // CenterDot
    let CenterDotBrush = new SolidBrush(Color.Red)
    let center =
        e.Graphics.FillEllipse(CenterDotBrush,-2.5f,-2.5f,5.0f,5.0f)
    center

    let dt = DateTime.Now
    let s = dt.Second
    let m = dt.Minute
    let h = dt.Hour
    let newS = rotate (float s/60.0*2.0*System.Math.PI) secondHand
    let newM = rotate (float m/60.0*2.0*System.Math.PI) minuteHand
    let newH = rotate (float h/12.0*2.0*System.Math.PI) hourHand
    let finalS = translate (Point (200, 200)) secondHand
    let finalM = translate (Point (200, 200)) minuteHand
    let finalH = translate (Point (200, 200)) hourHand
    ()

//win.Paint.Add myPaint
//win.Paint.Add(fun pea -> myPaint())
Application.Run win // start the event-loop