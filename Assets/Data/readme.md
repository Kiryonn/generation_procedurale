## Construction du fichier difficulties.json

- L'index de la liste est la difficulté.
- Les nombres entier représentent les règles actives.
- Les nombres se liront de la droite vers la gauche.

Cela permetra (si ont à le temps) de laisser aux joueurs controler les exercices qu'ils veulent travailler.

<u><b>Exemples:</b></u>

```json
[0, 5, 11, 15]
```

| nombre | représentation binaire |         règles actives |
|--------|-----------------------:|-----------------------:|
| 0      |                      0 |    Aucune règle active |
| 5      |                    101 |     règles 1,3 actives |
| 11     |                   1011 |   règles 1,2,4 actives |
| 15     |                   1111 | règles 1,2,3,4 actives |


## Construction des fichiers emails

- Les fichiers seront dans Data/Emails/
- Les fichiers seront nommé selon les règles actives mentioné si dessus 
  - <i>exemples: 0.json, 5.json, 11.json, 15.json</i>
- Les fichiers seont construits tel que:
```json5
{
  "any": { // type de contenu du mail
    "addresses": {
      "valid": [], // liste des emails valides
      "invalid": [] // liste des email invalides
    },
    "header": {
      "valid": [],  // les sujets de mail valides
      "invalid": [] // sujets invalides
    },
    "body": {
     "valid": [],  // contenu des e-mail valides
     "invalid": [] // contenu invalide
    },
    "footer": {
      "valid": [],  // remerciements, coordonées, ...
      "invalid": [] // la même mais invalide
    }
  },
  "art": {  // type de contenu
    ...
  }
}
```

## Construction du fichier tags.json

liste de type de contenu accepté par les fichiers email.
Le programme renverra une erreure et ignorera la catégorie si un type non mentionné dans ce fichier est trouvé pour éviter tout futurs problèmes
```json
["any", "art", "game", ...]
```