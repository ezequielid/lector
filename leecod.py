import pyttsx3

def hablar(mensaje):
    decir= input("ingrese en texto")
    engine = pyttsx3.init()
    engine.say(mensaje)
    engine.runAndWait()



hablar(decir)