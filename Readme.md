# Xamarin prototype
## Cryptocurrency address text to QR display with auto calculator

A Xamarin app to display a QR code corresponding to an address

Auto calculates prices

This is a personal prototype project with no commercial release

## Purpose of the app

As cryptocurrency gains traction, it occurred to me there is a minor problem with telling somebody your cryptocurrency address.
For example a typical Ethereum address is 42 characters long hexademical.
While it is possible to send an address by text or email, not everybody may want to do so during a face-to-face transaction.
This app would allow one to display their address for somebody else to scan with a barcode scanner thus avoiding having to say their number or email.

## Android version

Tested only on Android 9.0

## Features

* Uses ZXing to encode strings to barcodes
* Uses CoinMarketCap's free API to retrieve price data
* Auto calculates how much currency should be sent

![Screenshot](https://eempc.github.io/hosted_images//xamarin-app.PNG)

## Notes and Issues

* Xamarin Forms is very buggy, for weeks and weeks the emulator was broken. Each day I had to delete the emulator and create a new one. It's fixed now but was really annoying.
* Constant updates, like weekly or fortnightly, means you have to pray the update doesn't break the app.
* Need to install a lot of packages even to do something as simple as change the font because of iOS and Android idiosyncrasies.

## Built With

* [Visual Studio 2019](https://visualstudio.microsoft.com//) - Community Edition
* [Xamarin Forms (Android)](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/)
* SQLite PCL
* ZXing
* Coinmarketcap API
