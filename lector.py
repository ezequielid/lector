import PyPDF2
import pyttsx3
import keyboard
import tkinter as tk
from tkinter import filedialog

def cargar_pdf():
    global archivo_pdf
    archivo_pdf = filedialog.askopenfilename(filetypes=[("Archivos PDF", "*.pdf")])

    if archivo_pdf:
        cargar_contenido_pdf()

def cargar_contenido_pdf():
    with open(archivo_pdf, "rb") as pdf_file:
        pdf = PyPDF2.PdfReader(archivo_pdf)

    engine = pyttsx3.init()
    pausado = False

    def alternar_pausa(e):
        nonlocal pausado
        pausado = not pausado
        engine.stop()

    keyboard.on_press_key("p", alternar_pausa)

    for pagina in pdf.pages:
        contenido_pagina = pagina.extract_text()
        if not pausado:
            engine.say(contenido_pagina)
            engine.runAndWait()

    engine.stop()
    keyboard.unhook_all()

# Crear una ventana de Tkinter
ventana = tk.Tk()
ventana.title("Lector de PDF")

# Botón para cargar un archivo PDF
cargar_boton = tk.Button(ventana, text="Cargar PDF", command=cargar_pdf)
cargar_boton.pack()

archivo_pdf = None

# Ejecutar la ventana de Tkinter
ventana.mainloop()
