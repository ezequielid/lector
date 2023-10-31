import pyttsx3

def hablar(mensaje):
   
    engine = pyttsx3.init()
    engine.say(mensaje)
    engine.runAndWait()

decir= input("ingrese en texto")

hablar(decir)