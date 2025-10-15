using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class Test
{

	public struct Challenges {
		public int Nbr_de_1;
		public int Nbr_de_2;
		public int Nbr_de_3;
		public int Nbr_de_4;
		public int Nbr_de_5;
		public int Nbr_de_6;
		public int Brelan;
		public int Carre;
		public int Full;
		public int Petite_suite;
		public int Grande_suite;
		public int Yams;
		public int Chance;
		public int Total;
		public int TotalChalNbr;
		public List<string> ChalFait;
	}
	


	public struct Tour {
		public int id;
		public int id_player1;
		public int[] dice1;
		public string challenge1;
		public int score1;
		public int id_player2;
		public int[] dice2;
		public string challenge2;
		public int score2;
	}
	
	public struct final_result {
		public int id_player;
		public int bonus;
		public int score;
	}
	


	//Procédure TourJson (entier i, tableau du struct Tour TabTour, Fichier)
	//idée: écrire dans le fichier json chaque tour
	//entrée: 	tableau du struct Tour TabTour
	//		entier i
	//		Fichier
	//local: 	compteur i 
	//sortie:	void

	static void TourJson (int i, Tour[] TabTour, StreamWriter Fichier)
	{
		Fichier.WriteLine("\t{");
		Fichier.WriteLine("\t\"id\": "+TabTour[i].id+",");
		Fichier.WriteLine("\t\"results\": [");
		
		//Joueur1
		Fichier.WriteLine("\t{");
		Fichier.WriteLine("\t\t\"id_player\": "+TabTour[i].id_player1+",");
		Fichier.Write("\t\t\"dice\": [");
		for (int p=0; p<5; p++)
		{
				Fichier.Write(TabTour[i].dice1[p]);
				if (p<4)
				{
					Fichier.Write(",");
				}
		}
		Fichier.WriteLine("],");
		Fichier.WriteLine("\t\t\"challenge\": \""+TabTour[i].challenge1+"\",");
		Fichier.WriteLine("\t\t\"score\": "+ TabTour[i].score1);
		Fichier.WriteLine("\t},");
		
		//Joueur2
		Fichier.WriteLine("\t{");
		Fichier.WriteLine("\t\t\"id_player\": "+TabTour[i].id_player2+",");
		Fichier.Write("\t\t\"dice\": [");
		for (int p=0; p<5; p++)
		{
				Fichier.Write(TabTour[i].dice2[p]);
				if (p<4)
				{
					Fichier.Write(",");
				}
		}
		Fichier.WriteLine("],");
		Fichier.WriteLine("\t\t\"challenge\": \""+TabTour[i].challenge2+"\",");
		Fichier.WriteLine("\t\t\"score\": "+ TabTour[i].score2);
		Fichier.WriteLine("\t}");
		
		Fichier.WriteLine("\t]");
		Fichier.Write("\t}");
		if (i<12)
		{
			Fichier.Write(",");
		}
		Fichier.Write("\n");
	}
	
	//Procédure FichierJson (tableau du struct final_result TabResult, tableau du struct Tour TabTour, chaine de caractère J1, chaîne J2)
	//idée: créer un fichier json, et écrire dans le fichier la partie jouée
	//Entrée: 	tableau du struct final_result TabResult
	//		tableau du struct Tour TabTour
	//		chaine de caractère J1		//pseudo des joueurs
	//		chaîne J2 			//pseudo des joueurs
	//Local: 	compteur i 		//nb de tours
	//		compteur j 		//nb de joueur
	//		fichier fs 		//créer à l’aide de filestream
	//		Fichier 		//pour écrire dans fs
	//Sortie: 	void
	static void FichierJson (final_result[] TabResult, Tour[] TabTour, string J1, string J2)
	{
		DateTime Datee = DateTime.Now;
		string date = Datee.ToString("yyyy-MM-dd");
		string heure = Datee.ToString("HH-mm");
		FileStream fs = new FileStream("yams"+date+"_"+heure+".json",  FileMode.Create, FileAccess.Write);
		StreamWriter Fichier = new StreamWriter(fs);
		
		Fichier.WriteLine("{\n\t\"parameters\": {");
		Fichier.WriteLine("\t\"code\": \"groupe6-002\",");
		Fichier.WriteLine("\t\"date\": \""+date+"\"");
		Fichier.WriteLine("\t},");
		Fichier.WriteLine("\t\"players\": [");
		Fichier.WriteLine("\t {");
		Fichier.WriteLine("\t	\"id\": 1,");
		Fichier.WriteLine("\t	\"pseudo\": \""+J1+"\"");
		Fichier.WriteLine("\t },");
		Fichier.WriteLine("\t {");
		Fichier.WriteLine("\t	\"id\": 2,");
		Fichier.WriteLine("\t	\"pseudo\": \""+J2+"\"");
		Fichier.WriteLine("\t }");
		Fichier.WriteLine("\t],");
		Fichier.WriteLine("\t\"rounds\": [");
		for (int i=0; i<13; i++)
		{
			TourJson (i, TabTour, Fichier);
		}
		Fichier.WriteLine("\t ],");
		Fichier.WriteLine("\t\"final_result\": [");
		for (int j=0; j<2; j++)
		{
			Fichier.WriteLine("\t {");
			Fichier.WriteLine("\t 	\"id_player\": "+TabResult[j].id_player+",");
			Fichier.WriteLine("\t 	\"bonus\": "+TabResult[j].bonus+",");
			Fichier.WriteLine("\t 	\"score\": "+TabResult[j].score);
			Fichier.Write("\t }");
			if (j<1)
			{
				Fichier.WriteLine(",");
			}
		}
		Fichier.WriteLine("\n\t]");
		Fichier.WriteLine("}");
		
		Fichier.Close();
        	fs.Close();
	}

	//Procédure Nb1(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 1 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void

	static void Nb1(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for(int i=0; i<5; i++)
		{
			if (de[i]==1)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_1 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[0] = "";
	} 


	//Procédure Nb2(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 2 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void
	static void Nb2(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			if (de[i]==2)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_2 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[1] = "";
	} 


	//Procédure Nb3(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 3 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void
	static void Nb3(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			if (de[i]==3)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_3 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[2] = "";
	} 


	//Procédure Nb4(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 4 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void
	static void Nb4(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			if (de[i]==4)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_4 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[3] = "";
	} 


	//Procédure Nb5(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 5 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void
	static void Nb5(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			if (de[i]==5)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_5 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[4] = "";
	
	} 


	//Procédure Nb6(tableau d'entier de, tableau de string Tab, entier Joueur)
	//idée : calcule le nombre de 6 et stock les points gagnés dans la structure Challenges
	//entrée : 	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme 		//points gagnés
	//		entier i 		//compteur
	//sortie : 	void
	static void Nb6(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			if (de[i]==6)
			{
				somme = somme + de[i];
			}
		}
		Tab[Joueur].Nbr_de_6 = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].TotalChalNbr = Tab[Joueur].TotalChalNbr + somme;
		Tab[Joueur].ChalFait[5] = "";
	}


	//Procédure Brelan(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Calculer le nombre de dé identique, si il y a 3 ou plus alors le brelan est valide
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme
	//		entier nbidentique
	//		entier i		//compteur
	//sortie: 	void
	static void Brelan(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme = 0;
		for (int i = 0; i < de.Length; i++) 
		{			
		    int nbidentique = 1; 
		    int j = i + 1;

		    while (nbidentique < 4 && j < de.Length)
		    {
		        if (de[i] == de[j]) 
		        {
		            nbidentique++;
		        }
		        j++;
		    }

		    if (nbidentique >= 3) 
		    {
		        somme = de[i] * 3;
		    }
		}
		Tab[Joueur].Brelan = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].ChalFait[6] = "";

	}
	

	//Procédure Carre(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Calculer le nombre de dé identique, si il y a 4 ou plus alors le carré est valide
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier somme
	//		entier nbidentique
	//		entier i		//compteur
	//sortie: 	void
	static void Carre(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme = 0;
		for (int i = 0; i < de.Length; i++) 
		{			
		    int nbidentique = 1; 
		    int j = i + 1;

		    while (nbidentique < 5 && j < de.Length)
		    {
		        if (de[i] == de[j]) 
		        {
		            nbidentique++;
		        }
		        j++;
		    }

		    if (nbidentique >= 4) 
		    {
		        somme = de[i] * 4; 
		    }
		}
		Tab[Joueur].Carre = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].ChalFait[7] = "";
	}


	//Procédure Full(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Regarder si il y a 3 dés d'une valeur et 2 dés d'une autre valeur
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier nbid1
	//		entier nbid
	//		entier nbidentique
	//		entier i		//compteur
	//		entier j		//compteur
	//sortie: 	void
	static void Full(int[]de, Challenges[] Tab, int Joueur)
	{
		int nbid1=0;
		int nbid=0;
		for (int i=0; i<5; i++)
		{
			int nbidentique=0;
			for (int j=0; j<5; j++)
			{
				if(de[i]==de[j])
				{
					nbidentique = nbidentique+1;
				}
			}
			if (nbidentique==2)
			{
				nbid=2;
			}
			else if (nbidentique==3)
			{
				nbid1=3;
			}
		}
		if (nbid==2 && nbid1==3)
		{
			Tab[Joueur].Full = 25;
		}
		else
		{
			Tab[Joueur].Full = 0;
		}
		Tab[Joueur].Total = Tab[Joueur].Total + Tab[Joueur].Full; 
		Tab[Joueur].ChalFait[8] = "";
			
	}


	//Procédure PetiteSuite(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Trier le tableau de façon croissante puis vérifier s’il y a 1-2-3-4 ou 2-3-4-5 ou 3-4-5-6
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local :	entier temp
	//		entier nb
	//		entier i 		//compteur
	//		entier j 		//compteur
	//sortie:	 void
	public static void PetiteSuite(int[]de, Challenges[] Tab, int Joueur)
	{
        	for (int i = 0; i < de.Length - 1; i++)
        	{
			for (int j = 0; j < de.Length - 1 - i; j++)
			{
				if (de[j] > de[j + 1])
				{
					int temp = de[j];
					de[j] = de[j + 1];
				    	de[j + 1] = temp;
				}
			}
		}
		for (int i = 0; i < de.Length; i++) 
		{			
			int nb = 1; 
			int j = i + 1;

			while (nb < 5 && j < de.Length)
			{
				if (de[i] < de[j]) 
				{
				    nb++;
				}
				j++;
			}
			if (nb >= 4) 
			{
				Tab[Joueur].Petite_suite=30; 
			}
		}
		if (Tab[Joueur].Petite_suite != 30)
		{
			Tab[Joueur].Petite_suite=0;	
		}
		Tab[Joueur].Total = Tab[Joueur].Total + Tab[Joueur].Petite_suite;
		Tab[Joueur].ChalFait[9] = "";		
	}
	

	//Procédure GrandeSuite(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Trier le tableau de façon croissante puis vérifier s’il y a 1-2-3-4-5 ou 2-3-4-5-6
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local :	entier temp
	//		entier nb
	//		entier i 		//compteur
	//		entier j 		//compteur
	//sortie:	 void
	public static void GrandeSuite(int[]de, Challenges[] Tab, int Joueur)
	{
        	for (int i = 0; i < de.Length - 1; i++)
        	{
			for (int j = 0; j < de.Length - 1 - i; j++)
			{
				if (de[j] > de[j + 1])
				{
					int temp = de[j];
					de[j] = de[j + 1];
				    	de[j + 1] = temp;
				}
			}
		}
		for (int i = 0; i < de.Length; i++) 
		{			
			int nb = 1; 
			int j = i + 1;

			while (nb < 6 && j < de.Length)
			{
				if (de[i] < de[j]) 
				{
				    nb++;
				}
				j++;
			}
			if (nb >= 5) 
			{
				Tab[Joueur].Grande_suite=40; 
			}
		}
		if (Tab[Joueur].Grande_suite != 40)
		{
			Tab[Joueur].Grande_suite = 0;
		}
		Tab[Joueur].Total = Tab[Joueur].Total + Tab[Joueur].Grande_suite;
		Tab[Joueur].ChalFait[10] = "";		
	}
	

	//Procédure Yams(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Regarder si il y a 5 chiffres de même valeur
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local : 	entier val
	//		entier nbid
	//		entier i 		//compteur
	//sortie : 	void
	public static void Yams(int[]de, Challenges[] Tab, int Joueur)
	{
		int val=de[0];
		int nbid=1;
		for (int i=1; i<5; i++)
		{
			if (de[i]==val)
			{
				nbid=nbid+1;
			}
		}
		if (nbid==5)
		{
			Tab[Joueur].Yams = 50; 
		}
		else
		{
			Tab[Joueur].Yams = 0;
		}
		Tab[Joueur].Total = Tab[Joueur].Total + Tab[Joueur].Yams;
		Tab[Joueur].ChalFait[11] = "";
	}
	
	
	//Procédure Chance(tableau d’entier de, tableau de la struct Challenges Tab, entier Joueur)
	//idée: Calcul la somme des dés
	//entrée :	tableau d'entier de 		//contient les dés tirés
	//		tableau de string Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier Joueur 		//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//local :	 entier somme
	//		entier i 		//compteur
	//sortie : 	void
	public static void Chance(int[]de, Challenges[] Tab, int Joueur)
	{
		int somme=0;
		for (int i=0; i<5; i++)
		{
			somme = somme + de[i];
		}
		Tab[Joueur].Chance = somme;
		Tab[Joueur].Total = Tab[Joueur].Total + somme;
		Tab[Joueur].ChalFait[12] = "";
	} 





	//Fonction Lancer (entier N) tableau d’entiers
	//idée : Lancer un nombre N de dés choisis aléatoirement et les place dans un tableau
	//entrée :	 entier N		//nombre de chiffre à lancer
	//local : 	rnd 			//permet de choisir aléatoirement un chiffre
	//		entier dice 		//chiffre aléatoire compris entre 1 et 6
	//		entier i		//compteur
	//sortie :	 tableau d’entier des		//stock les dés tirés
	static int[] Lance(int N)
	{
		Random rnd = new Random();
			
		int[] des = new int [5];
		for (int i=0; i < N; i++)
		{
			int dice = rnd.Next(1, 7);
			des[i] = dice;
		}
		return des;
	}


	//Fonction lanceSupp(tableau d’entiers des) : tableau d’entiers
	//Idée: Demander au joueur s’il veut rejeter ses dés, puis renvoyer le tableau de dés avec les nouveaux (ou anciens) dés
	//Entrée:	tableau d’entiers des [5]
	//Local: 	entier i 		//compteur
	//		entier j 		//compteur pour afficher les dés
	//		chaine de caractères deschange 		//indice des dés à changer, si =”0” alors renvoyer des
	//		tableau de caractères Tab 		//mettre l’indice des dés dans un tableau
	//		tableau d’entier b 			//le nouveau dés
	//Sortie: 	tableau d’entiers des [5]
	static int[] lanceSupp(int[] des)
	{
		for (int i = 0; i<2; i++)
		{
			//AFFICHAGE DES
			Console.WriteLine("\n========DES========");
			Console.WriteLine("===================");

			for (int z = 0; z < 5; z++)
			{
				Console.Write(" " + des[z] + " ");
			}
			Console.WriteLine("\n===================");
			Console.WriteLine("===================");	


			//DEMANDER LESQUELS CHANGER
			Console.WriteLine("Quel(s) dé(s) changer ? (les mettre à la suite sans espace et mettre 0 si aucun dé à changer)");
			string a = Console.ReadLine();
			while (a.Length > 5)
			{
				Console.WriteLine("ERREUR : Il n'y a que 5 dés\nRemettez un/des nouveau(x) dé(s) à changer");
				a = Console.ReadLine();
			}
			if (a == "0")
			{
				return des;
			}

			char[] temp = a.ToCharArray();
		
			foreach (char val in temp)
			{
				int vall = (int)Char.GetNumericValue(val);

				if (val != '1' && val != '2' && val != '3' && val != '4' && val != '5')
				{
					Console.WriteLine("ERREUR : valeur incorrecte\nRemettez un nouveau dé à changer\nSi plusieurs erreurs ont été faites, ce message s'affichera pour chaque erreur faite");
					vall = int.Parse(Console.ReadLine());
				}
							
				int p=vall-1;
				int[] b =  Lance(1);
				des[p] = b[0];	
			}
			
		}
		return des;
	}


	//Fonction ChoixChal(tableau de chaines Tab, entier joueur, tableau d’entier des) : chaine
	//idée : Permet à l’utilisateur de choisir un challenge
	//entrée :	tableau de chaines Tab		//tableau contenu dans la structure challenges car dépend du joueur
	//		entier joueur			//permet de connaître le joueur en train de jouer pour avoir accès à ses données
	//		tableau d’entier des		//contient les dés tirés
	//local : 	chaine val		//sert à afficher les challenges disponibles
	//		entier Choix		//challenge choisi par le joueur
	//sortie : 	chaine 			//retourne le challenge choisi pour le JSON
	static string ChoixChal(Challenges[] Tab, int joueur,int[] des)
	{
		Console.WriteLine("\nChoisir le challenge\n");
		foreach (string val in Tab[joueur].ChalFait)
		{
			if (val != "")
			{
				Console.WriteLine(" | " + val);
			}
		}
		Console.Write("\n");
		Console.WriteLine("\n Entrez le code du challenge choisi");
		string Choix = Console.ReadLine();
		if (Choix == "1")	
		{
			if (Tab[joueur].Nbr_de_1 == 999)
			{
				Nb1(des, Tab, joueur);
				return "nombre1";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}
		}
		else if (Choix == "2")
		{
			if (Tab[joueur].Nbr_de_2 == 999)
			{
				Nb2(des, Tab, joueur);
				return "nombre2";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}
		}
		else if (Choix == "3")
		{
			if (Tab[joueur].Nbr_de_3 == 999)
			{
				Nb3(des, Tab, joueur);
				return "nombre3";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "4")
		{
			if (Tab[joueur].Nbr_de_4 == 999)
			{
				Nb4(des, Tab, joueur);
				return "nombre4";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "5")
		{
			if (Tab[joueur].Nbr_de_5 == 999)
			{
				Nb5(des, Tab, joueur);
				return "nombre5";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "6")
		{
			if (Tab[joueur].Nbr_de_6 == 999)
			{
				Nb6(des, Tab, joueur);
				return "nombre6";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "7")
		{
			if (Tab[joueur].Brelan == 999)
			{
				Brelan(des, Tab, joueur);
				return "brelan";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "8")
		{
			if (Tab[joueur].Carre == 999)
			{
				Carre(des, Tab, joueur);
				return "carre";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "9")
		{
			if (Tab[joueur].Full == 999)
			{
				Full(des, Tab, joueur);
				return "full";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "10")
		{
			if (Tab[joueur].Petite_suite == 999)
			{
				PetiteSuite(des, Tab, joueur);
				return "petite";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "11")
		{
			if (Tab[joueur].Grande_suite == 999)
			{
				GrandeSuite(des, Tab, joueur);
				return "grande";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "12")
		{
			if (Tab[joueur].Yams == 999)
			{
				Yams(des, Tab, joueur);
				return "yams";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else if (Choix == "13")
		{
			if (Tab[joueur].Chance == 999)
			{
				Chance(des, Tab, joueur);
				return "chance";
			}
			else
			{
				Console.WriteLine("\n\nERREUR : Le challenge a déjà été choisi");
				ChoixChal(Tab, joueur, des);
			}

		}
		else
		{
			Console.WriteLine("\n\nERREUR : Mauvais nombre choisi");
			ChoixChal(Tab, joueur, des);
		}
		return null;
	}




	//Procédure Recap(tableau de la struct Challenges Tab, entier Joueur, chaine J1, chaine J2)
	//Idée : Afficher un récapitulatif de la partie en cours
	//Entrée : 	tableau de la struct Challenges Tab 		// tableau de score de chaque challenge
	//		entier Joueur 		// id du joueur
	//		chaine J1 		//pseudo joueur 1
	//		chaine J2		//pseudo joueur 2
	//Local : /
	//Sortie : /
	static void Recap(Challenges[] Tab, int Joueur, string J1, string J2)
	{
		Console.WriteLine("=======RECAP=======");
		if (Joueur == 0)
		{
			Console.WriteLine(J1);
		}
		else
		{
			Console.WriteLine(J2);
		}
		Console.WriteLine("===================");
		if (Tab[Joueur].Nbr_de_1==999){
			Console.WriteLine("Nombre de 1 : ");
		}
		else{Console.WriteLine("Nombre de 1 : {0}", Tab[Joueur].Nbr_de_1);}

		if (Tab[Joueur].Nbr_de_2==999){
			Console.WriteLine("Nombre de 2 : ");
		}
		else{Console.WriteLine("Nombre de 2 : {0}", Tab[Joueur].Nbr_de_2);}

		if (Tab[Joueur].Nbr_de_3==999){
			Console.WriteLine("Nombre de 3 : ");
		}
		else{Console.WriteLine("Nombre de 3 : {0}", Tab[Joueur].Nbr_de_3);}

		if (Tab[Joueur].Nbr_de_4==999){
			Console.WriteLine("Nombre de 4 : ");
		}
		else{Console.WriteLine("Nombre de 4 : {0}", Tab[Joueur].Nbr_de_4);}

		if (Tab[Joueur].Nbr_de_5==999){
			Console.WriteLine("Nombre de 5 : ");
		}
		else{Console.WriteLine("Nombre de 5 : {0}", Tab[Joueur].Nbr_de_5);}

		if (Tab[Joueur].Nbr_de_6==999){
			Console.WriteLine("Nombre de 6 : ");
		}
		else{Console.WriteLine("Nombre de 6 : {0}", Tab[Joueur].Nbr_de_6);}

		if (Tab[Joueur].Brelan==999){
			Console.WriteLine("Brelan : ");
		}
		else{Console.WriteLine("Brelan : {0}", Tab[Joueur].Brelan);}
			
		if (Tab[Joueur].Carre==999){
			Console.WriteLine("Carré : ");
		}
		else{Console.WriteLine("Carré : {0}", Tab[Joueur].Carre);}
			
		if (Tab[Joueur].Full==999){
			Console.WriteLine("Full : ");
		}
		else{Console.WriteLine("Full : {0}", Tab[Joueur].Full);}

		if (Tab[Joueur].Petite_suite==999){
			Console.WriteLine("Petite suite : ");
		}
		else{Console.WriteLine("Petite suite : {0}", Tab[Joueur].Petite_suite);}

		if (Tab[Joueur].Grande_suite==999){
			Console.WriteLine("Grande suite : ");
		}
		else{Console.WriteLine("Grande suite : {0}", Tab[Joueur].Grande_suite);}
		
		if (Tab[Joueur].Yams==999){
			Console.WriteLine("Yams : ");
		}
		else{Console.WriteLine("Yams : {0}", Tab[Joueur].Yams);}
			
		if (Tab[Joueur].Chance==999){
			Console.WriteLine("Chance : ");
		}
		else{Console.WriteLine("Chance : {0}", Tab[Joueur].Chance);}	
		
		Console.WriteLine("===================");
		Console.WriteLine("Total : {0}", Tab[Joueur].Total);
		Console.WriteLine("===================");
		Console.WriteLine("===================");
		Console.Write("\n");
	}


	public static void Main()
	{
		//demande des pseudos
		Console.WriteLine("======PSEUDO=======");
		Console.WriteLine("===================");
		Console.Write("Pseudo joueur 1 : ");
		string J1=Console.ReadLine();
		Console.Write("Pseudo joueur 2 : ");
		string J2=Console.ReadLine();
		Console.WriteLine("===================");
		Console.WriteLine("===================");
		Console.Write("\n \n");

		//stockage tableau contenu de la partie
		Challenges j1 = new Challenges();
		Challenges j2 = new Challenges();
		j1.Nbr_de_1 = 999;
		j1.Nbr_de_2 = 999;
		j1.Nbr_de_3 = 999;
		j1.Nbr_de_4 = 999;
		j1.Nbr_de_5 = 999;
		j1.Nbr_de_6 = 999;
		j1.Brelan = 999;
		j1.Carre = 999;
		j1.Full = 999;
		j1.Petite_suite = 999;
		j1.Grande_suite = 999;
		j1.Yams = 999;
		j1.Chance = 999;
		j1.Total = 0;
		j1.TotalChalNbr = 0;
		j1.ChalFait = new List<string>(){"1: Nombre de 1", "2: Nombre de 2", "3: Nombre de 3", "4: Nombre de 4", "5: Nombre de 5", "6: Nombre de 6", "7: Brelan", "8: Carré", "9: Full", "10: Petite suite", "11: Grande suite", "12: Yams", "13: Chance"};

		j2.Nbr_de_1 = 999;
		j2.Nbr_de_2 = 999;
		j2.Nbr_de_3 = 999;
		j2.Nbr_de_4 = 999;
		j2.Nbr_de_5 = 999;
		j2.Nbr_de_6 = 999;
		j2.Brelan = 999;
		j2.Carre = 999;
		j2.Full = 999;
		j2.Petite_suite = 999;
		j2.Grande_suite = 999;
		j2.Yams = 999;
		j2.Chance = 999;
		j2.Total = 0;
		j2.TotalChalNbr = 0;
		j2.ChalFait = new List<string>(){"1: Nombre de 1", "2: Nombre de 2", "3: Nombre de 3", "4: Nombre de 4", "5: Nombre de 5", "6: Nombre de 6", "7: Brelan", "8: Carré", "9: Full", "10: Petite suite", "11: Grande suite", "12: Yams", "13: Chance"};

		Challenges[] TabChallenges = new Challenges [2] {j1, j2};
		//TabChallenges[0].Full = 40;

		//début tour
		Tour[] TabTour = new Tour[13];
		for (int z=1; z<=13; z++)
		{
			TabTour[z-1].id=z;
			for (int i=0; i<=1; i++)
			{
				int Joueur = i;
				Console.Write("\n\n");
				Recap(TabChallenges, Joueur, J1, J2);
				int[] dees = Lance(5);
				int[] d;

				d = lanceSupp(dees);

				Console.WriteLine("\n\n=====DÉS FINALS====");
				Console.WriteLine("===================");

				for (int j=0; j<5; j++)
				{
					Console.Write(" "+d[j]+" ");
				}
				Console.WriteLine("\n===================");
				Console.WriteLine("===================");

				string ChallengeChoisi = ChoixChal(TabChallenges, Joueur, d);
				Console.Write("\n\n");
				Recap(TabChallenges, Joueur, J1, J2);
				
				//Remplir la structure du joueur1
				if (i==0)
				{
					TabTour[z-1].id_player1=i+1;
					TabTour[z-1].dice1=d;
					TabTour[z-1].challenge1=ChallengeChoisi;
					
					if (ChallengeChoisi=="nombre1")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_1;
					}
					else if (ChallengeChoisi=="nombre2")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_2;
					}
					else if (ChallengeChoisi=="nombre3")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_3;
					}
					else if (ChallengeChoisi=="nombre4")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_4;
					}
					else if (ChallengeChoisi=="nombre5")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_5;
					}
					else if (ChallengeChoisi=="nombre6")
					{
						TabTour[z-1].score1=TabChallenges[i].Nbr_de_6;
					}
					else if (ChallengeChoisi=="brelan")
					{
						TabTour[z-1].score1=TabChallenges[i].Brelan;
					}
					else if (ChallengeChoisi=="carre")
					{
						TabTour[z-1].score1=TabChallenges[i].Carre;
					}
					else if (ChallengeChoisi=="full")
					{
						TabTour[z-1].score1=TabChallenges[i].Full;
					}
					else if (ChallengeChoisi=="petite")
					{
						TabTour[z-1].score1=TabChallenges[i].Petite_suite;
					}
					else if (ChallengeChoisi=="grande")
					{
						TabTour[z-1].score1=TabChallenges[i].Grande_suite;
					}
					else if (ChallengeChoisi=="yams")
					{
						TabTour[z-1].score1=TabChallenges[i].Yams;
					}
					else if (ChallengeChoisi=="chance")
					{
						TabTour[z-1].score1=TabChallenges[i].Chance;
					}
				}
				
				//Remplir la structure du joueur2
				if (i==1)
				{
					TabTour[z-1].id_player2=i+1;
					TabTour[z-1].dice2=d;
					TabTour[z-1].challenge2=ChallengeChoisi;
					
					if (ChallengeChoisi=="nombre1")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_1;
					}
					else if (ChallengeChoisi=="nombre2")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_2;
					}
					else if (ChallengeChoisi=="nombre3")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_3;
					}
					else if (ChallengeChoisi=="nombre4")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_4;
					}
					else if (ChallengeChoisi=="nombre5")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_5;
					}
					else if (ChallengeChoisi=="nombre6")
					{
						TabTour[z-1].score2=TabChallenges[i].Nbr_de_6;
					}
					else if (ChallengeChoisi=="brelan")
					{
						TabTour[z-1].score2=TabChallenges[i].Brelan;
					}
					else if (ChallengeChoisi=="carre")
					{
						TabTour[z-1].score2=TabChallenges[i].Carre;
					}
					else if (ChallengeChoisi=="full")
					{
						TabTour[z-1].score2=TabChallenges[i].Full;
					}
					else if (ChallengeChoisi=="petite")
					{
						TabTour[z-1].score2=TabChallenges[i].Petite_suite;
					}
					else if (ChallengeChoisi=="grande")
					{
						TabTour[z-1].score2=TabChallenges[i].Grande_suite;
					}
					else if (ChallengeChoisi=="yams")
					{
						TabTour[z-1].score2=TabChallenges[i].Yams;
					}
					else if (ChallengeChoisi=="chance")
					{
						TabTour[z-1].score2=TabChallenges[i].Chance;
					}
				}
			}
		}
		
		
		// affichage des résultats
		Console.WriteLine("\n\n===================");
		Console.WriteLine("===================");
		Console.WriteLine("Résultats finaux");
	
		final_result[] TabResult = new final_result[2];
		for (int k = 0; k <= 1; k++)
		{
			TabResult[k].id_player=k+1;
			int Joueur = k;
			if (TabChallenges[Joueur].TotalChalNbr >= 63)
			{
				TabChallenges[Joueur].Total = TabChallenges[Joueur].Total + 35;
				Console.WriteLine("\n\nBonus reçu");
				TabResult[k].bonus=35;
			}
			else
			{
				Console.Write("\n\n");
				TabResult[k].bonus=0;
			}
			Recap(TabChallenges, Joueur, J1, J2);
			TabResult[k].score=TabChallenges[k].Total;
		}

		if (TabChallenges[0].Total > TabChallenges[1].Total)
		{
			Console.WriteLine(J1 + " a gagné(e) la partie");
		}
		else if (TabChallenges[0].Total < TabChallenges[1].Total)
		{
			Console.WriteLine(J2 + " a gagné(e) la partie");
		}
		else
		{
			Console.WriteLine("Egalité entre les deux joueurs");
		}
		FichierJson (TabResult, TabTour, J1, J2);
	}

}
