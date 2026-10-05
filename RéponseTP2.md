### Réponse aux questions du TP N°2 - BONNIER Gabin

**__Question N°1 :__ <br>** 
Celui qui a fabriqué ```_id``` est l'outil ```mongoimport``` en générant l'```objectId```
<br> Pour comparer avec l'id 4 lui était géré par le serveur via un système d'autoincrément alors que dans celui ci c'est géré en vérifiant s'il existe.

**__Question N°2 :__** <br>
En SQL, on ne peut pas mettre un liste directement dans une colonne. On aurait du faire un relation entre ```jeux``` et ```plateformes```
Comme la requête suivante : 
```sql
SELECT j.titre
FROM jeux j
INNER JOIN jeu_plateforme jp ON j.id = jp.jeu_id
INNER JOIN plateformes p ON p.id = jp.plateforme_id
WHERE p.nom = 'Switch';
```
MongoDb permet de simplifier cette requête, car il cherche si l'élément est déjà dans la liste sans l'intérêt de faire une seule jointure.

**__Question N°3 :__**
L'utilisateur ```pixelhub``` est le compte root qui est créer au démarrage du contenuer Docker, donc MongoDB le stocke dans la base de système ```admin```.
<br>Si on ne met pas le ```--authentificationDatabase admin```, mongo cherche le compte dans la base ```pixelhub``` par défaut et refuse la connexion parce qu'il ne le trouve pas.

**__Question N°4 :__**
C'est pratique pour prototyper vite fait au début, mais c'est surtout dangereux car MongoDb acceptera tout sans jamais "raler" donc ça peut vite être le bazard dans la base de données avec plein de confusion.

**__Question N°5 :__**
Le résultat ne renvoie aucune erreur car MongoDB gère les collections de façon dynamique et est sensible à la casse. Pour lui, ```Jeux``` est simplement le nom d'une autre collection. Il a cherché dans cette collection qui est vide, et il a donc renvoyé un ensemble vide. Interroger une collection inexistante n'est pas une erreur pour MongoDB, donc il ne lève aucune exeption.

**__Question N°6 :__**
En tapant : ```shell pixelhub> db.jeux.updateOne({ titre: "Valorant" }, { note: 4.4 })``` le terminal me renvoie l'erreur suivante : ```ERROR MongoInvalidArgumentError: Update document requires atomic operators```,  car la méthode ```updateOne``` attend obligatoirement des opérateurs de mise à jour atomique, alors que si on lui donne un objet direct, il ne sais tpas s'il doit effacer un seul champ ou toutes les données, ce qui entrainerait la destruction de toutes les données du jeux.
<br>
Personnelement, j'utiliserais la commande suivante : ```db.jeux.replaceOne({ titre: "Valorant" }, { note: 4.4 })```, au lieux de ~~```updateOne```~~ je mettrais ```replaceOne```


**__Question N°7 :__**
La commande nécessaire pour faire ça est la suivante : 
```sql 
ALTER TABLE jeux ADD COLUMN nb_votes INTEGER;
```

**__Question N°8 :__**
On met le mot ```mongo``` qu'a un seul endroit comme ça si on veut changer de "systeme" on doit juste le changer à un seul endroit et pas partout dans le code.


**__Questio N°9 :__**
Exeption obtenu : ```System.FormatException: Element 'nombreCircuits' does not match any field or property of class PixelHub.Api.Models.Game``` <br>
Le driver cherche un champ qui n'existe pas dans la classe ```Game```.

**__Question N°10 :__**
L'attribut permet de régler l'erreur mais ça prend le risque de perdre toute les données

**__Question N°11 :__**
Comparaison des trois approches :
```[BsonIgnoreExtraElements]``` :
<br>- **Avantages :** Très simple et rapide à mettre en place
<br>- **Inconvénients :** Perte pure et simple d'informations ; les champs spécifiques stockés en base deviennent totalement invisibles pour l'application C#.

```[BsonExtraElements]``` :
<br>- **Avantages :** Aucune perte de données, n'importe quel champ imprévu ou spécifique est capturé dynamiquement sans faire planter l'application.
<br>- **Inconvénients :** Perte du typage fort : les champs supplémentaires sont manipulés comme des paires clé/valeur non sécurisées (object), ce qui complique les contrôles dans le code et nécessite des astuces (comme un Dictionary) pour la sérialisation JSON de l'API.

Hiérarchie des classes (```FpsGame : Game```, ```CourseGame : Game```):
<br>- **Avantages :** Typage fort complet (autocomplétion, validation à la compilation, propriétés propres à chaque genre).
<br>- **Inconvénients :** Très rigide et lourd à maintenir : il faut créer et faire évoluer une classe par variante, et configurer la gestion du polymorphisme côté MongoDB (via un champ discriminateur _t).

**__Question N°12 :__**
Requête SQL équivalente :
```sql 
SELECT genre, AVG(note) AS "noteMoyenne", COUNT(*) AS nombre
FROM jeux
GROUP BY genre
ORDER BY "noteMoyenne" DESC;
```
Non, mongoDB n'apporte pas quelque chose. Pour des calcules tabulaires simples comme des regroupements (```GROUP BY```), des moyennes (```AVG```) ou des comptages (```COUNT```).