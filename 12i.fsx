(*
    Emil Bæk Henriksen
    wsl-798
    Opgave 12i
*)
open System.Windows.Forms
open System.Drawing
open System
let height = 400
let width = 400
let size = Size (width,height)
let win = new Form()
win.BackColor <- Color.White
win.ClientSize <- size
win.Text <- sprintf "Ze Watch"
/// <summary>The function transpose makes the center 0,0 
/// for every point passed to it</summary>
/// <param name="x">x is a Point to translate to a new position</param>
/// <remarks>Works with points nothing else</remarks>
/// <returns>A Point</returns>
let transpose (x:Point) : Point =
  let center = Point(width/2,height/2)
  Point (center.X+x.X,center.Y+x.Y)
/// <summary>The degree calculates the position of the clock hands</summary>
/// <param name="t">a DateTime.Now object</param>
/// <remarks>Cannot be a DateTime without Now</remarks>
/// <returns>A Point List</returns>
let degree (t:System.DateTime) =
  let handPos a r=
    int(a*r)
  let dPos d r =
    Point(handPos (Math.Cos(d-(Math.PI/2.))) r,handPos (Math.Sin(d-(Math.PI/2.))) r)
  let hour = (Math.PI*2.)/12.*float(t.Hour)
  let min = (Math.PI*2.)/60.*float(t.Minute)
  let sec = (Math.PI*2.)/60.*float(t.Second)
  [dPos hour (float(width/6)); dPos min (float(width/5)); dPos sec (float(width/4))]
/// <summary>the function nums creates the numbers inside the watch, 
/// based on the width of the screen</summary>
/// <remarks>Could be optimized, buuut maybe later</remarks>
/// <returns>A unit</returns>
let nums =
  let handPos a =
    int(a*float(width/5))
  let dPos d =
    Point((handPos(Math.Cos(d-(Math.PI/2.))))-5,(handPos (Math.Sin(d-(Math.PI/2.))))-5)
  for i in 1..12 do
    let hour = (Math.PI*2.)/12.*float(i)
    let l = new Label ()
    win.Controls.Add l
    l.Location <- transpose (dPos hour)
    l.Text <- string i
    l.ForeColor <- Color.Black
    l.BackColor <- Color.Transparent
    l.Width <- 20
/// <summary>circl creates the outline of the watch, based on width</summary>
/// <remarks>Has only been tested on a limited range of widths </remarks>
/// <returns>a Unit</returns>
let circl (e:PaintEventArgs) : unit =
  let pen = new Pen ( Color.Red )
  pen.Width <- float32(6)
  let r = width/2
  let pos = transpose(Point(-(r/2),-(r/2)))
  e.Graphics.DrawEllipse(pen, pos.X,pos.Y, r, r)
win.Paint.Add circl
/// <summary>the function hands creates and updates the clock hands each tick.</summary>
/// <remarks>This is a compressed function</remarks>
/// <returns>a unit</returns>
let hands =
  for i in 0..2 do
    let clock ( e : PaintEventArgs ) : unit =
      let cols = [|Color.Black;Color.Blue;Color.Red|]
      let pen = new Pen ( cols.[i] )
      pen.Width <- float32(3)
      let h = [| (transpose (Point(0,0))); transpose (degree DateTime.Now).[i]|]
      e.Graphics.DrawLines ( pen , h)
    win.Paint.Add clock
let label = new Label ()
win.Controls.Add label
label.Location <- transpose (Point (0-(width/8),(width/4)))
label.BackColor <- Color.Transparent
label.ForeColor <- Color.Black
label.Text <-  sprintf "%02d:%02d:%02d" DateTime.Now.Hour DateTime.Now.Minute DateTime.Now.Second
let timer = new Timer ()
timer.Interval <- 1000 // create an event every 1000 millisecond
timer.Enabled <- true
timer.Tick.Add (fun e -> label.Text <-  sprintf "%02d:%02d:%02d" DateTime.Now.Hour DateTime.Now.Minute DateTime.Now.Second;win.Refresh())

System.Windows.Forms.Application.Run win