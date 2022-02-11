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

Votre équipe de développement vient de se voir commandé le développement d’une API permettant de gérer une centrale de réservation de véhicule.

Le Product Owner (PO) de l’équipe a déjà réalisé le recueil des besoins et défini les différentes règles métiers.
Règles métiers :

#### Cas d’utilisation :
- Le client s’identifie ou créer un compte
- Le client rentre la date de début et la date de fin de la location qu’il
souhaite effectuer
- La liste des véhicules disponibles est présentée au client
- Le client sélectionne une voiture parmi la liste fournie
- La réservation est créée
- Lorsque le client rend la voiture, la facture avec le prix final est générée

#### Description des objets :

**Véhicule**
- Un véhicule est représenté par une immatriculation unique
- Il possède une marque, un modèle et une couleur
- Un véhicule dispose d’un prix de réservation qui permet de couvrir les
frais de dossier et le nettoyage du véhicule
- Il dispose également d’un tarif kilométrique permettant de couvrir
l’usure du véhicule
- Un nombre de chevaux fiscaux

**Client**
- Un client est défini par nom, un prénom
- Il est important de pouvoir de posséder la date de naissance du client
ainsi que la date d’obtention de son permis de conduire pour savoir quel
sera le prix de l’assurance
- Le numéro de permis du client est enregistré afin de pouvoir être fournis
aux autorités si un problème devait survenir.
   
#### Réservation

- Une réservation est une association entre un client et un véhicule
- Une réservation a toujours une date de début et une date de fin
- Un client ne peut réserver qu’un seul véhicule à la fois
- Le véhicule souhaité par le client doit être disponible entre la date de
début et la date de fin
- Un client doit au moins avoir 18 ans et posséder le permis de conduire
pour réserver un véhicule
- Un conducteur de moins de 21 ans ne peut pas louer un véhicule
possédant 8 chevaux fiscaux ou plus
- Un conducteur entre 21 et 25 ans ne peut louer que des véhicules de
moins de 13 chevaux fiscaux.
- Lors de la réservation, le client doit estimer le nombre de km qu’il
compte faire. Le prix de location est calculé en fonction de cette estimation. Bien entendu si cette estimation est dépassée ou surestimé un réajustement est effectué lors du rendu de véhicule
- Le prix d’une location est le suivant : prix de base + prix au km * nb de km

## Travail demandé

### 1. En vous basant sur ces règles métiers,vous allez devoir créer les scénarios Gherkin correspondants, puis implémenter les tests et le code de production correspondant en respectant le cycle BDD.

Pour simplifier l’implémentation, et car ce n’est pas le but premier du module, nous n’utiliserons pas de base de données.
Par contre, nous ferons comme si notre librairie pouvait être branchée à une base de données.

Il faudra donc simuler des données existantes (Clients, Voitures, Réservations). Cela signifie qu’il faudra implémenter un moyen pour fournir à votre librairie un jeu de données pour exécuter vos tests.

Pour plus de lisibilité, vous pouvez organiser vos scenarios en utilisant plusieurs fichiers .feature.

### 2. En vous basant sur le document fourni en annexe, créer un pipeline dans Azure Devops afin de compiler votre solution et lancer les tests et générer la Living Documentation. Je vous conseille de mettre en place cette
 
intégration continue dès que vous avez créer votre premier scenario et créer votre repo Git
Important :

- Comme pour les TPs précédents, pour simplifier la mise en œuvre nous
travaillerons avec des projets de type libraires .Net Core sans affichage
dans un premier temps.
- Selon votre avancement, et votre aisance technique, vous pouvez ajouter
en plus un projet console à votre solution qui utilisera votre librairie en conditions « réelles » : récupération des données utilisateurs, appels à votre librairie et affichage des résultats.

Attention dans cas à votre architecture, le code de cette application
console ne doit contenir aucune logique hormis celle nécessaire à la mise en œuvre de votre librairie.

## Livrables
- Un lien vers un repo Git contenant : Vos projets de tests et code de production
- Un lien vers votre intégration continue Azur Devops
 