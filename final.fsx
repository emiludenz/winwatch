open System.Windows.Forms
open System.Drawing
open System


let width = 400
let height = 400
let size = Size(width,height)
let win = new Form () // make a windowform
win.ClientSize <- size
win.BackColor <- Color.White




let translate (p : Point) : Point =
  let center = Point (width/2,height/2)
  Point (center.X + p.X, center.Y + p.Y)

let digitalClock = new Label ()
win.Controls.Add digitalClock 
digitalClock.Width <- 200
digitalClock.Location <- translate (new Point (0,100))
digitalClock.Text <- string System.DateTime.Now // get present time and date
digitalClock.ForeColor <- Color.Black
digitalClock.BackColor <- Color.Transparent

let circl (e:PaintEventArgs) : unit = 
  let pen = new Pen ( Color.Red )
  let d = width/2
  let pos = translate(Point(0-(d/2),0-(d/2)))
  e.Graphics.DrawEllipse(pen, pos.X,pos.Y,d,d)
win.Paint.Add circl
let degree =
  let deg = (2.*Math.PI/60.)*float(DateTime.Now.Second)
  Point(int(Math.Cos(deg)*100.), int(Math.Sin(deg)*100.))


let clockS ( e : PaintEventArgs ) : unit =
  let pen = new Pen ( Color.Black )
  let min = Point (int(Math.Cos(float(System.DateTime.Now.Minute)))+100, int(Math.Sin(float(System.DateTime.Now.Minute)))+100)
  let m = [| Point (width/2,width/2) ; min |]
  e.Graphics.DrawLines ( pen , m )
win.Paint.Add clockS
*)
let timer = new Timer()
timer.Interval <- 1000 // create an event every 1000 millisecond
timer.Enabled <- true // activate the timer
timer.Tick.Add (fun e -> digitalClock.Text <- string System.DateTime.Now;win.Refresh())


System.Windows.Forms.Application.Run win