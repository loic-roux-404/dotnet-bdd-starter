# BDD

- [Installation dotnet](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Installation dotnet sdk](https://dotnet.microsoft.com/en-us/download/dotnet/sdk-for-vs-code)
- install recommanded extensions and restart vscode
- Build calculator with `dotnet build SpecFlowCalculator`
- Test with `dotnet test SpecFlowCalculator.Specs`

To recreate template from blank folder :
- Add Specflow `dotnet add package SpecFlow --version 3.9.40`
- `dotnet new --install SpecFlow.Templates.DotNet::3.9.40`
- `dotnet new specflowproject`

# Enoncé

## 1. Cas Nominal

Votre équipe de développement vient de se voir commandé le développement d’une API permettant de désigner le vainqueur d’un scrutin majoritaire.

Le Product Owner (PO) de l’équipe s’est réuni avec les clients afin de rédiger la première User Story du projet :
En tant que client de l’API à la clôture d’un scrutin majoritaire Je veux calculer le résultat du scrutin

Pour obtenir le vainqueur du vote le Product Owner a également ajouté à la User Story
 les critères d’acceptances suivant :

- Pour obtenir un vainqueur, le scrutin doit être clôturé
- Si un candidat obtient > 50% des voix, il est déclaré vainqueur du scrutin
- On veut pouvoir afficher le nombre de votes pour chaque candidat et le
pourcentage correspondant à la clôture du scrutin.
- Si aucun candidat n'a plus de 50%, alors on garde les 2 candidats
correspondants aux meilleurs pourcentages et il y aura un deuxième tour
de scrutin
- Il ne peut y avoir que deux tours de scrutins maximums
- Sur le dernier tour de scrutin, le vainqueur est le candidat ayant le
pourcentage de vote le plus élevé
- Si on a une égalité sur un dernier tour, on ne peut pas déterminer de
vainqueur
En vous basant sur ces critères d’acceptance, vous allez devoir créer les scénarios Gherkin correspondants, puis implémenter les tests et le code de production correspondant en respectant le cycle BDD.
Je vous conseille de commencer par les cas d’utilisation nominaux pour construire la base de votre projet, puis d’ajouter un par un les scenarios afin de prendre en compte tous les critères d’acceptations.
Note : Comme pour le TP précédent, pour simplifier la mise en œuvre nous travaillerons avec des projets de type libraires .Net (pas d’affichage).
    
## 2. Critères manquants

Vous aurez surement remarqué qu’ils manquent certains critères d’acceptances pour pouvoir gérer tous les cas d’utilisations :

- Gestion des égalités sur le 2ème et 3ème candidat sur un premier tour
- Gestion du vote blanc
Dans cette deuxième partie, à vous de prendre le rôle de Product Owner et de décider les critères d’acceptations que vous voulez mettre en place pour gérer ces deux cas.
Ajoutez les scenarios manquants et les implémentations associées.
