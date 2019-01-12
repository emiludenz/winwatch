open System.Windows.Forms
open System.Drawing
open System

let _w = 400
let _h = 400

let win = new Form()
win.BackColor <- Color.White
win.ClientSize <- Size (_w , _h)
win.Text <- sprintf "Maa Watch"
win.TransformPosition

  






(*let clockH ( e : PaintEventArgs ) : unit =
  let pen = new Pen ( Color.Black )
  let hour = Point (int(Math.Cos(float(System.DateTime.Now.Hour)))+100, int(Math.Sin(float(System.DateTime.Now.Hour)))+100)
  let h = [| Point (_w/2,_h/2) ; hour |]
  e.Graphics.DrawLines ( pen , h )
win.Paint.Add clockH

let clockM ( e : PaintEventArgs ) : unit =
  let pen = new Pen ( Color.Black )
  let min = Point (int(Math.Cos(float(System.DateTime.Now.Minute)))+100, int(Math.Sin(float(System.DateTime.Now.Minute)))+100)
  let m = [| Point (_w/2,_h/2) ; min |]
  e.Graphics.DrawLines ( pen , m )
win.Paint.Add clockM
*)

let now = System.DateTime.Now
(*let clockS ( e : PaintEventArgs ):unit =
  let pen = new Pen ( Color.Black)
  
  let d = (float(now.Second) * (360./60.))
  let x = int(Math.Cos(d)*100.)
  let y = int(Math.Sin(d)*100.)
  let sec = Point (x,y)
  let s = [| Point (_w/2,_h/2) ; sec |]
  e.Graphics.DrawLines ( pen , s )
win.Paint.Add clockS*)


(*
let s = [for i in 1..12 -> i]
for i in s do printfn "%i %f" (i)(float(i)*alpha)
1 30.000000
2 60.000000
3 90.000000
4 120.000000
5 150.000000
6 180.000000
7 210.000000
8 240.000000
9 270.000000
10 300.000000
11 330.000000
12 360.000000



*)
let label = new Label ()
win.Controls.Add label
label.Width <- _w/3
label.Location <- new Point ((_w/2)-(label.Width/2),int(float(_h)/1.2))
label.BackColor <- Color.Black




let timer = new Timer ()
label.Text <- string DateTime.Now
timer.Interval <- 1000 // create an event every 1000 millisecond
timer.Enabled <- true
timer.Tick.Add (fun e -> label.Text <- string System.DateTime.Now)



let circl (e:PaintEventArgs) : unit = 
  let pen = new Pen ( Color.Red )
  e.Graphics.DrawEllipse(pen, 100,100,200, 200)
win.Paint.Add circl

System.Windows.Forms.Application.Run win