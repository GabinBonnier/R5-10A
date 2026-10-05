1. ça permet de ne pas modifier le fichier de configuration tout le temp, en rajoutant des services.
2. Le seul service qui a pas de volume monté est **"Redis"**, il n'a pas de volume monté car les données ne doivent pas être conservé entre chaque montage démontage du docker, par exemple en faisant "docker compose down", toute les données des volumes non montés ne sont pas conservés.
3. Il s'agit d'une relation entre deux port, un port hôte et un port conteneur.
   <br> _(15432:5432)_ --> **15432 :** port d'écoute | **5432 :** c'est le port standard interne à docker pour PostgreSQL
4. **En local :** En local les données dans le fichier ```docker-compose.yml``` ne sont pas dérangeant car c'est un fichier qui restera uniquement en local donc ne sera partagé à personne. <br>
    **En production :** En production les données dans le fichier ```docker-compose.yml``` sont dérangeant car les données sont partagés en public. Pour éviter cela il faudrait mettre dans un fichier du type ```.env``` pour éviter que ce soit partagé en public.
5. La commande ```docker compose exec``` permet d'éxercuter une commande à l'intérieur du conteneur qui est déjà en cours d'éxecution, directement dans le processur du conteneur actif <br>
La commande ```redis-cli``` permet d'éxecuter aussi une commande à l'intérieur du conteneur mais dans le conteneur ```pixelhub_redis``` et pas sur le pc en local
6. La connexion avec ```Host=localhost``` fonctionne car dotnet tourne directement sur ma machine Hôte (mon mac).
7. Non, les trois joueurs ne se dupliquent pas, parce que ils sont ajoutés seulement si la table est vide.