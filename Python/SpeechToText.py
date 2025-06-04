import speech_recognition as sr
import keyboard
import time

#Globale Vaiblen
all_spoken_words = []  
stop_listening_func = None  # Funktion zum Stoppen des Hintergrund-Zuhörens
recognizer = sr.Recognizer()  # Instanz des Recognizers
microphone = sr.Microphone()  # Instanz des Mikrofons
KEY_TO_HOLD = 'space'  # Die Taste die gedrückt und gehalten werden muss (spacebar)
currently_listening_flag = False # Zeigt an ob wir gerade aktiv zuhören

def speech_callback(r, audio_data):
    global all_spoken_words, KEY_TO_HOLD

    if not keyboard.is_pressed(KEY_TO_HOLD):
        print("Taste wurde losgelassen bevor Audio verarbeitet wurde.") # Optional: Debug-Ausgabe
        return

    print("Audiodaten empfangen versuche Spracherkennung...")
    try:
        text = r.recognize_google(audio_data, language="de-DE")
        print(f"In Echtzeit erkannt: \"{text}\"")

        words = text.lower().split()  
        all_spoken_words.extend(words)
        print(f"Aktuelle Wortliste ({len(all_spoken_words)} Wörter): {all_spoken_words}")

    except sr.UnknownValueError:
        print("Spracherkennung konnte nichts verstehen.")
    except sr.RequestError as e:
        print(f"Fehler bei der Anfrage an den Spracherkennungsdienst; {e}")
    except Exception as e:
        print(f"Ein unerwarteter Fehler in der Callback-Funktion: {e}")

def start_listening_process():
    global stop_listening_func, recognizer, microphone, currently_listening_flag, KEY_TO_HOLD

    if currently_listening_flag:
        return

    print(f"\nTaste '{KEY_TO_HOLD}' gedrückt. Starte das Zuhören...")
    currently_listening_flag = True

    # Starte das Zuhören im Hintergrund.
    # 'phrase_time_limit=5' bedeutet, dass die Callback-Funktion spätestens
    # nach 5 Sekunden Inaktivität oder kontinuierlichem Sprechen aufgerufen wird.
    # Dies hilft, auch längere Sätze in Segmenten zu erfassen.
    stop_listening_func = recognizer.listen_in_background(microphone, speech_callback, phrase_time_limit=4)
    print("Zuhören ist jetzt aktiv. Sprich!")

def stop_listening_process():
    global stop_listening_func, all_spoken_words, currently_listening_flag, KEY_TO_HOLD

    if not currently_listening_flag: # War nicht am Zuhören
        return

    print(f"\nTaste '{KEY_TO_HOLD}' losgelassen. Stoppe das Zuhören.")
    currently_listening_flag = False

    if stop_listening_func:
        stop_listening_func(wait_for_stop=False)  # Stoppt den Hintergrund-Thread sofort
        stop_listening_func = None

    if all_spoken_words:
        print("\n--- Zusammenfassung der erkannten Wörter ---")
        print(f"Insgesamt {len(all_spoken_words)} Wörter erkannt: {all_spoken_words}")
        print("-------------------------------------------\n")
    else:
        print("Es wurden keine Wörter während dieser Sitzung erkannt.")
    
    print(f"Halte '{KEY_TO_HOLD}' erneut gedrückt zum Sprechen oder drücke 'esc' zum Beenden.")


if __name__ == "__main__":
    print("Python Skript zur Sprachaufnahme gestartet.")
    print(f"Halte die '{KEY_TO_HOLD}'-Taste gedrückt um aufzunehmen.")
    print("Lass die Taste los um die Aufnahme zu stoppen und die Wörter anzuzeigen.")
    print("Drücke 'esc' um das Skript jederzeit zu beenden.")

    # Einmalige Kalibrierung für Umgebungsgeräusche beim Start
    try:
        with microphone as source:
            print("\nBitte sei kurz still, kalibriere Umgebungsgeräusche...")
            recognizer.adjust_for_ambient_noise(source, duration=2) # Dauer der Kalibrierung
            print("Kalibrierung abgeschlossen. Bereit zum Zuhören.\n")
    except Exception as e:
        print(f"Fehler bei der Mikrofoninitialisierung oder Kalibrierung: {e}")
        print("Stelle sicher, dass ein Mikrofon angeschlossen ist und die notwendigen Berechtigungen erteilt wurden.")
        print("Möglicherweise musst du auch 'PyAudio' korrekt installieren (siehe Hinweise oben).")
        exit()

    try:
        while True:
            if keyboard.is_pressed(KEY_TO_HOLD):
                if not currently_listening_flag:
                    start_listening_process()
            elif currently_listening_flag: # Taste ist nicht (mehr) gedrückt, aber wir haben zugehört
                stop_listening_process()

            if keyboard.is_pressed('esc'):  # Überprüfe, ob 'esc' gedrückt wurde zum Beenden
                if currently_listening_flag: # Falls gerade gehört wird, erst stoppen
                    stop_listening_process()
                print("\n'esc' gedrückt. Skript wird beendet.")
                break
            
            time.sleep(0.01)  # Kurze Pause, um die CPU-Last zu reduzieren

    except ImportError:
        print("Die 'keyboard' Bibliothek wurde nicht gefunden. Bitte installiere sie mit: pip install keyboard")
        print("Auf Linux könntest du Root-Rechte benötigen (sudo python ...).")
    except Exception as e:
        print(f"Ein unerwarteter Hauptfehler ist aufgetreten: {e}")
    finally:
        # Sicherstellen, dass der Hintergrund-Listener gestoppt wird, falls er noch läuft
        if stop_listening_func:
            stop_listening_func(wait_for_stop=False)
        
        print("\n--- Finale Liste aller jemals erkannten Wörter ---")
        if all_spoken_words:
            print(f"Insgesamt {len(all_spoken_words)} Wörter: {all_spoken_words}")
        else:
            print("Keine Wörter wurden im gesamten Verlauf erkannt.")
        print("Programm sauber beendet. Auf Wiedersehen! 👋")