# MantyharjunKirjastonMusiikkiMellakka

UNITY VERSIO 2020.3.22f1 !

## Rest api v1

The api is minimal, only having a single endpoint which supports POST and GET requests.

On an unsuccessful request the status code is in the range of 400<=x<600.

On an successful (status 200) GET request the endpoint should return a JSON object which will look like the following, without the comments:

```json5
[
	{
		// An integer of this scoreboard record in UNIX time.
		"time": 0,
		"name": "Unescaped'Player\"Name"
	}
]
```

The client can also make a POST request with just one of the record objects to insert a new record.

## Gitin perusteet

- `git clone git@github.com:ljoonal/MantyharjunKirjastonMusiikkiMellakka.git`
- `git add tiedostonimi` - lisää tiedoston "valmistelvavaksi"
- `git commit -m "viesti"` - lisää valmistettavat tiedostot historiaan
- `git push` - Työntää muutokset serverille
- `git pull` vetää muutokset serveriltä
