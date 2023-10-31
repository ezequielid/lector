from flask import Flask, render_template, request
import pyttsx3

app = Flask(__name__)

@app.route("/", methods=["GET", "POST"])
def hablar():
    if request.method == "POST":
        mensaje = request.form["mensaje"]
        engine = pyttsx3.init()
        engine.say(mensaje)
        engine.runAndWait()

    return render_template("index.html")

if __name__ == "__main__":
    app.run(debug=True)

#flask run --host=0.0.0.0 --port=8080