﻿namespace _4Time.FrontEnd
{
    internal class DadJokes
    {
        private static Random random = new Random();
        private static int autismCounter = 0; // Zähler für die Anzahl der aufgerufenen Witze
        private static Dictionary<int, string> DadJokesDic = new Dictionary<int, string>
        {
            {1, "Was ist grün und steht vor der Tür? Ein Klopfsalat!"},
            {2, "Was sagt der große Stift zum kleinen Stift? Wachs-Mal-Stift!"},
            {3, "Warum können Geister so schlecht lügen? Weil sie leicht zu durchschauen sind!"},
            {4, "Was macht ein Clown im Büro? Faxen!"},
            {5, "Was ist ein Keks unter einem Baum? Ein schattiges Plätzchen!"},
            {6, "Wie nennt man einen Bumerang, der nicht zurückkommt? Stock."},
            {7, "Was ist rot und schlecht für die Zähne? Ein Ziegelstein."},
            {8, "Was sitzt auf einem Baum und schreit \"Aha\"? Ein Uhu mit Sprachfehler."},
            {9, "Warum trinken Mäuse keinen Alkohol? Weil sie Angst vor dem Kater haben."},
            {10, "Was ist orange und wandert durch die Wüste? Eine Wanderine."},
            {11, "Geht ein Cowboy zum Friseur. Kommt er raus, ist sein Pony weg."},
            {12, "Was ist weiß und stört beim Essen? Eine Lawine."},
            {13, "Wie nennt man einen Hund, der zaubern kann? Labrakadabrador."},
            {14, "Was sagt die Null zur Acht? Schicker Gürtel!"},
            {15, "Warum summen Bienen? Weil sie den Text nicht kennen."},
            {16, "Was ist ein Ritter ohne Helm? Willhelm."},
            {17, "Was macht ein Pirat am Computer? Die Enter-Taste drücken."},
            {18, "Treffen sich zwei Jäger. Beide tot."},
            {19, "Was ist gelb und schießt? Eine Banone."},
            {20, "Wie heißt ein Spanier ohne Auto? Carlos."},
            {21, "Was sagt ein Gen, wenn es ein anderes trifft? Halogen."},
            {22, "Was ist das Lieblingsessen von Piraten? Kapern."},
            {23, "Warum hat der Bäcker keine Frau? Weil er eine Mehlallergie hat."},
            {24, "Was ist süß und hängt am Baum? Ein Schokobaum."},
            {25, "Welches Getränk trinken Firmenchefs? Leitungswasser."},
            {26, "Was ist schwarz-weiß und springt von Eisberg zu Eisberg? Ein Springuin."},
            {27, "Sagt die eine Wand zur anderen: Wir treffen uns in der Ecke!"},
            {28, "Was ist ein Pilzsammler? Ein Mann, der gerne in die Pilze geht."},
            {29, "Warum sind Friedhöfe so beliebt? Weil dort jeder rein will."},
            {30, "Was ist ein Thermometer, das hinfällt? Fahrenheit."},
            {31, "Was macht ein Dieb im Getreidefeld? Er klaut den Mais."},
            {32, "Wie nennt man ein helles Mammut? Hellmut."},
            {33, "Was ist der Unterschied zwischen einem Fußballer und einem Fußgänger? Der Fußballer geht bei Grün und der Fußgänger bei Rot."},
            {34, "Was essen Autos am liebsten? Parkplätzchen."},
            {35, "Sitzen zwei Kühe auf der Wiese und stricken. Kommt ein Schaf vorbei und fragt: \"Warum strickt ihr denn?\" Sagt die eine Kuh: \"Wolle\""},
            {36, "Was ist grün, klein und dreieckig? Ein kleines grünes Dreieck."},
            {37, "Was sagt der Hammer zum Daumen? Ich schlag dir was vor."},
            {38, "Warum können Fische nicht Fahrrad fahren? Weil sie keine Hände haben."},
            {39, "Was ist ein Cowboy ohne Pferd? Ein Sattelschlepper."},
            {40, "Was ist das Gegenteil von Reformhaus? Reh hinterm Haus."},
            {41, "Wie nennt man einen Käfer, der auf die Toilette muss? Pipi-Käfer."},
            {42, "Was ist gelb und kann nicht schwimmen? Ein Bagger."},
            {43, "Sagt der Pessimist: \"Schlimmer geht's nicht!\" Sagt der Optimist: \"Doch!\""},
            {44, "Was ist der Unterschied zwischen einem Anzug und einem Kondom? Bei ersterem kommen die besten Stücke später rein."},
            {45, "Was ist schwarz und weiß und rot? Ein Zebra mit Sonnenbrand."},
            {46, "Warum fliegen Vögel im Winter in den Süden? Weil es zu Fuß zu weit ist."},
            {47, "Was macht eine Bombe im Bordell? Puff!"},
            {48, "Was ist der Lieblingssport der Schafe? Määh-Staffellauf."},
            {49, "Fragt der Richter den Angeklagten: \"Wollen Sie einen Verteidiger?\" Antwortet der Angeklagte: \"Lieber einen schnellen Fluchtwagen!\""},
            {50, "Was ist süß, klebrig und läuft durch die Wüste? Ein Karamel."},
            {51, "Wie nennt man einen armen reichen Mann? Ein Vermögensverwalter."},
            {52, "Was macht ein Mathelehrer im Garten? Wurzeln ziehen."},
            {53, "Was sagt der Teigling, bevor er in den Ofen kommt? \"Mir ist aber schon warm!\""},
            {54, "Warum hat ein Elefant rote Augen? Damit er sich besser im Kirschbaum verstecken kann. Hast du schon mal einen Elefanten im Kirschbaum gesehen? Siehst du, gute Tarnung!"},
            {55, "Was ist grün und klopft an die Tür? Ein Klopfsalat."},
            {56, "Was liegt am Strand und ist schlecht gelaunt? Eine Miesmuschel."},
            {57, "Was ist der Unterschied zwischen einem Lehrer und einem Zug? Der Lehrer sagt: \"Einsteigen, aufpassen!\", der Zug sagt: \"Aufpassen, einsteigen!\""},
            {58, "Was ist rund und hat Durchfall? Ein Hula-Hoop-Reifen."},
            {59, "Wie nennt man einen dicken Schriftsteller? Einen Buchautor."},
            {60, "Was ist ein studierter Bauer? Ein Ackerdemiker."},
            {61, "Was macht ein Null-Euro-Schein in Afrika? Er verhungert."},
            {62, "Was ist grün und rennt durch den Wald? Ein Rudel Gurken."},
            {63, "Warum gehen Ameisen nicht in die Kirche? Weil sie Insekten sind."},
            {64, "Was ist der Unterschied zwischen einem Bäcker und einem Teppich? Der Bäcker muss um vier aufstehen, der Teppich kann liegen bleiben."},
            {65, "Was ist der Lieblingsstift von Imkern? Der Wachsmalstift."},
            {66, "Was sagt ein hungriges Krokodil, das einen Clown gefressen hat? Schmeckt komisch!"},
            {67, "Wo wohnt die Katze? Im Miezhaus."},
            {68, "Was kommt nach Elch? Zwölch."},
            {69, "Bitte 200 Gramm Leberwurst von der groben Fetten. Tut mir Leid, die hat heute Berufsschule."},
            {70, "Was ist bunt und läuft über den Tisch davon? Ein Fluchtsalat!"},
            {71, "Was ist grün, sauer und versteckt sich vor der Polizei? Ein Essig-Schurke."},
            {72, "Was sagt man über einen Spanner, der gestorben ist? Der ist weg vom Fenster."},
            {73, "Treffen sich zwei Magneten. Sagt der eine: Was soll ich heute bloß anziehen?"},
            {74, "Was essen Supermodels, wenns schnell gehen muss? Ein Laufsteak."},
            {75, "Was heißt Sonnenuntergang auf Finnisch? Helsinki."},
            {76, "Hast du ein Bad genommen? – Warum, fehlt eins?"},
            {77, "Ich bin es leid, ständig hier herumzuhängen, sagte die Glühbirne und brannte durch."},
            {78, "Treffen sich zwei Voyeure. Fragt der eine: Und was machst du heute noch so? Sagt der andere: Mal gucken."},
            {79, "Wie heißt der chinesische Verkehrsminister? Um Lei Tung."},
            {80, "Kriegen sich zwei Glatzen in die Haare."},
            {81, "Was macht man mit einem Hund ohne Beine? Um die Häuser ziehen."},
            {82, "Zwei Bekloppte beißen in Eisenbahnschienen. Sagt der eine: Boa, sind die hart! Sagt der andere: Geh doch da drüben hin, da ist ne Weiche!"},
            {83, "Wie heißt die Frau von Herkules? Frau Kules."},
            {84, "Kommt ein Einarmiger in einen Secondhandladen."},
            {85, "Was macht ein Zauberer, wenn er wütend ist? Er zaubert aus der Wut!"},
            {86, "Was sagt der Schuh zum Hut? Du gehst mir auf den Kragen!"},
            {87, "Warum kann ein Fahrrad nicht stehen? Weil es nur zwei Reifen hat!"},
            {88, "Was ist braun und schwimmt im Wasser? Ein U-Brot."},
            {89, "Was sagt der Apfel zum Wurm? Na, aus dir wird auch kein Schmetterling mehr!"},
            {90, "Warum ist ein Tennisball schneller als ein Baseball? Er hat einen Aufschlag!"},
            {91, "Was hat vier Räder und fliegt? Ein Müllauto im Dienst!"},
            {92, "Warum essen Computer nie? Weil sie schon genug Chips haben!"},
            {93, "Warum tragen Hühner keine Unterwäsche? Weil ihre Eier ständig im Korb liegen!"},
            {94, "Wie nennt man einen Hund ohne Beine? Wurst."},
            {95, "Was machen zwei Nullen im Kino? Sie schauen sich eine Nullnummer an!"},
            {96, "Wie nennt man ein Kalb, das bei einem Rodeo gewonnen hat? Ein Rodeokalb!"},
            {97, "Was macht eine Ente auf einem Fahrrad? Sie tritt in die Pedale!"},
            {98, "Wie nennt man einen verärgerten Pilz? Einen Wut-zling!"},
            {99, "Was machen zwei Glühbirnen, wenn sie sich treffen? Sie gehen auf Lichterloh!"},
            {100, "Warum essen Elefanten keine Computer? Weil sie zu viele Bytes haben!"}
        };

        internal static string GetRandomDadJoke()
        {
            if (DadJokesDic.Count == 0)
            {
                return "How did u get here? " + (autismCounter < Int32.MaxValue ? ++autismCounter : "Bro es reicht...");
            }
            int randomIndex = random.Next(1, DadJokesDic.Count + 1);
            string currentJoke;
            if (!DadJokesDic.TryGetValue(randomIndex, out string? value))
            {
                randomIndex = DadJokesDic.First().Key;
                currentJoke = DadJokesDic.First().Value; // Gibt den ersten Witz zurück, falls der zufällige Index nicht existiert
            }
            else currentJoke = value;
            DadJokesDic.Remove(randomIndex); // Entfernt den Witz, um Duplikate zu vermeiden
            return currentJoke;
        }
    }
}
