
#### **Les injections**

Nous allons mettre en place le moyen de vous proteger contre les ataque de type d'injection.

Sur chaque entrée (un champ ou un utilisateur va marquer une donnée) effectuer par un utilisateur on va vérifier, filtrer, et effectuer un échappement de caractères, pour vérifier de l'integrité de la données.

Cette solution sera mise en place à l'aide de requêtes préparées (AddWithValue)
Avec cette méthode cela vous protegera contre ces deux type d'injection le SQLi et le XSS stockées

* ##### **SQLi**

Il s'agit d'attaques visant la base de données. Les moyens d'entrée sont tous simplement les différentes requête utiliser pour communiquer entre la base de données et l'utilisateur. Et au vu de l'application il y aura beaucoup de requete que des utilisateurs pourront effectuer.
Le problème de ces attaques, c'est s'il arrive a entréer dedans les hacker pourront lire, altérer ou détruire la base de données.

* ##### **XSS**

Cross-Site Scripting est une attaque qui permet d'injecter des donnés dans une page web qui va l'interpréter. Elle est de forme HTML ou JavaScript. Elle est implémenté dans les application grâce à une entrée (exemple une zone commentaire).
Ce type d'attaque est prévue pour visée les utilisateurs qui viendront sur votre application et notre votre application en elle même.
Si un de vos apprenant est mal intentionné. Il pourrait mettre en commentaire à un de vos cours un script qui pourra recuperer par exemple des cookies de connexion d'utilisateur qui arrivera dans la page car le navigateur interpretera le commentaire et effectura le script.
En faisant la méthode expliquer un peu plus haut, ce type d'attaque sera arreté.
Mais ceci est une des attaque XSS possible.


####	**CSP**
Une Content Security Policy (CSP) ou stratégie de sécurité de contenu a pour but d’améliorer la sécurité des sites web. Pour cela, elle détecte et réduit un certain nombre d’attaques dont les attaques Cross-Site Scripting (XSS) et les injections de code

Grâce à la CSP, on va pouvoir restreindre l'accès aux ressources atteignables des domaines ou sous-domaines sous forme d'autorisation.  
On peut le mettre en place via une configuration du serveur afin d'ajouter un en-tête (header) HTTP Content-Security-Policy aux réponses. Mais pour éviter de toucher au serveur, on pourra le mettre en place via les balises <meta> HTML en lui indiquant les règles qu'on voudra mettre en place.

Permettront de maitriser les **requêtes silencieuses**. Elles permettent de demander au navigateur d’émettre des requêtes sans passer par l’exécution de code JavaScript ou CSS. Ce qui est potentiellement dangereux si elles ne sont pas maitrisées. Car elles peuvent conduire à la fuite d'information, comme exploiter les failles CRSF, voir des attaques DDoS. 

---------------------------

### **L’entête sécurisé (CORS / TLS / HTTP / HTTPS)**

#### **SOP: Same origin Policy**

Dans le but de d’encadrer les origines des requêtes faites à notre application, nous allons mettre en place des règles grâce aux **CORS (Cross origin ressource sharing)**:
Nous Mettrons en place des Request with Credentials, validant et limitant l’ouverture de notre API.

Cela nous sécurisera des **CSRF (Cross Site Request Forgery)**:
Qui tente de faire exécuter une requête à l’utilisateur en la cachant dans un formulaire par exemple.

Nous mettrons également en place un protocole **HTTPS** afin de protéger l'intégrité ainsi que la confidentialité des données lors du transfert d'informations entre l'ordinateur de l'internaute et le site.

Nous y parviendrons par le biais des **TLS (Transport Layer Security)**   


### **Le moindre privilège**

Pour une meilleure sécurisation de votre application, nous allons appliquer le principe du moindre privilège. Cela fait référence à un concept de sécurité dans lequel on accorde à un utilisateur le niveau d’accès (ou les permissions) minimum requis pour accomplir son travail. Mais aussi qu'une fonction n'aura que les données et ressources nécessaire à son déroulement et rien d'autre.
Cela permet de réduire la surface exposée aux cyberattaques.
On éliminera les privilèges d’administrateur local inutiles et on s’assurera que tous les utilisateurs humains et non humains dispose uniquement des privilèges nécessaires

Pour faciliter la mise en place des droits pour chaque utilisateur, nous allons appliquer la méthode **RBAC** (Role based Access Control).
Nous allons créer des rôles avec des autorisations, droits. Pour faciliter la gestion des utilisateurs en compartimentant les accès donnés, que cette personne pourra atteindre. Ce qui fera une couche de sécurité sur les accès donnée. L'avantage supplémentaire est de gagner du temps par la suite, pour éviter de devoir attribuer des droits l'un après l'autre à chaque apprenant, formateur ou employer. Il suffira d'attribuer les rôles aux personnes qui pourront être modifiées.
Ça optimisera l’efficacité opérationnelle, protègera les données des risques de fuite ou de vol, réduit le travail d’administration et d’assistance informatique.


### **La politique RGPD**

Une plateforme comme la vôtre est une cible majeure contre les **attaques malveillantes**, donc elle doit se protéger davantage.

La plateforme de formation en ligne présente des données à caractère personnel de ses utilisateurs et ainsi doit obligatoirement respecter le **Règlement Général sur la Protection des Données** (RGPD), qui établi des règles sur la collecte et l’utilisation des données de l'utilisateur.
Il a été conçu autour de 3 objectifs :

* renforcer les droits des personnes
* responsabiliser les acteurs traitant des données
* crédibiliser la régulation grâce à une coopération renforcée entre les autorités de protection des données

Ainsi éviter le vol de données en respectant toutes les recommandations citées ci-dessus.
Ce qui incite la plateforme a mettre en place une stratégie de sécurité adaptée.

### **Journalisation**

La journalisation est un élément primordial pour assurer la sécurité des traitements de données, qui est obligation posée par l’article 5 du RGPD. 
Il est nécessaire d’avoir un système de journalisation (c’est-à-dire un enregistrement dans des « fichiers journaux » ou « logs ») des activités des utilisateurs, des anomalies et des événements liés à la sécurité. Ces journaux doivent conserver les évènements sur une période glissante ne pouvant excéder six mois (sauf obligation légale, ou risque particulièrement important) :
- La journalisation doit concerner, au minimum, les accès des utilisateurs en incluant leur identifiant, la date et l’heure de leur connexion, et la date et l’heure de leur déconnexion.
- Dans certains cas, il peut être nécessaire de conserver également le détail des actions effectuées par l’utilisateur, les types de données consultées et la référence de l’enregistrement concerné.
- Informer les utilisateurs de la mise en place d’un tel système, après information et consultation des représentants du personnel.
- Protéger les équipements de journalisation et les informations journalisées contre les accès non autorisés, notamment en les rendant inaccessibles aux personnes dont l’activité est journalisée.
- Établir des procédures détaillant la surveillance de l’utilisation du traitement et examiner périodiquement les journaux d’événements pour y détecter d’éventuelles anomalies.
- Assurer que les gestionnaires du dispositif de gestion des traces notifient, dans les plus brefs délais, toute anomalie ou tout incident de sécurité au responsable de traitement.
- Notifier toute violation de données à caractére personnel à la CNIL et, sauf exception prévue par le RGPD, aux personnes concernées pour qu’elles puissent en limiter les conséquences

A noter que le système de logs devra être mis en lien avec une API de tickets afin de faciliter le **bug bounty** (traquer des bugs) des équipes.  

------------------------------

## Stratégie de sécurisation de l'authentification


Ce rapport traite des stratégies de sécurité à mettre en place concernant l'authentification et les autorisations.

### Politique de gestion des facteurs Auth, focus sur l'User

La stratégie d'autorisations reposera sur 3 rôles séparés au minimum :

    -low-privilege (simple visiteur)   
    -user (utilisateur enregistré et authentifié)   
    -admins (administrateurs/debug/dev team)   

L'ANSI recommande évidemment un mot de passe fort, mais aussi que celui-ci ne soit pas trop contraignant pour l'utilisateur, afin de l'inciter à retenir plusieurs mots de passe usuels, pratiques et fort lui-même.
Il est recommandé d'ajouter une couche d'authentification multi-composante supplémentaire( différent types de FACTEURS ).  
L'authentification multi-facteurs tendant à se démocratiser, notre politique envers les utilisateurs sera donc:  
  
    -rappel constant des règles de sécurité(phishing, communication avec employés etc)
    -proposer et encourager la pratique de la double authentification avec des solutions de tokens temporaires telles que:
    Authy , Google authenticator, etc.
    -rendre l'authentification multi-facteur OBLIGATOIRE pour les formateurs
    -limiter les essais consecutifs d'authentification
    -A l’inscription : vérification d’e mail 
    -Forcer l’utilisateur à respecter une politique de Mot de passe:
    -12 caractères minimum
    -numériques+alphabétique+caractères spéciaux(1 minimum de chaque)
    -pas de suites logiques de nombre (456789)
    -pas plus de 2 caractères semblables à la suite (aaa – 222)
        
	note: bien que l'ANSI recommande un mot de passe le plus long possible une limite maximale arbitraire de 
	50 caractères sera mise en place,afin de limiter en première intention dans cette couche,
	l'impact des attaques DDOS.


A l’inscription, le mot de passe utilisateur subira un Hachage SHA256 du mot de passe utilisateur avec un salage fort et UNIQUE, il n'y aura donc aucun mot de passe stocké en textuel sur nos serveurs, afin de miniser l'exploitabilité de la couche Data.
Cette décision implique l'absence de possibilité de RECUPERATION de mot de passe, et la mise en place d'un système de REINITIALISATION de mot de passe.
A l'aide de BitCrypt.

Nous mettrons en place une demande d’autorisation à code alphanumérique 6 caractères par l’email de contact quand l’User se connecte pour la première fois depuis un nouveau périphérique ET l'envoi d’une alerte mail PASSIVE (ou par téléphone selon le choix de l’User) quand une tentative de connexion à lieu depuis un nouveau périphérique.

Les sessions seront toujours temporaires par mesure sanitaire de sécurité, mais seront par défaut longues afin de ne pas entraver l'utilisation régulière, par défaut la validité d'une session authentifiée, sera révoquée:  
    -tout les mois  
    -à la moindre MAJ de l'application  
Nous mettrons en place une possibilité de révocation de session et de facteurs d'authentification d'urgence pour les administrateurs.

Chaque requête concernant l'authentification devra être journalisée avec des erreures type et une validation type.

Afin d'habituer l'Utilisateur à gérer sa sécurité de compte, tout en améliorant l'ergonomie d'utilisation, nous proposerons à l'Utilisateur des préférences de contact pour:  
	-le reset password  
	-les alertes de conexion  


### A propos des Admins MODIFIER

Tout les logins spéciaux de tâches d’administration seront :

	-chiffrés avec une clef synchrone changeante régulièrement en entreprise, 
	les employés devront changer leur mot de passe crypté régulièrement.

A terme, et afin de faciliter le travail constant des equipes, sans compromettre la sécurité des accès sensibles, nous recommandons la mise en place d'une sécurité multi facteurs avec facteurs physiques du type:

    1 Badge + mot de passe fort sur des machines dédiées
    2 Biométrie + badge + mot de passe fort sur des machines dédiées
    
de changer constament le facteur "badge" qui sera remis en main propre aux employés acrédités toutes les deux semaines par exemple.

