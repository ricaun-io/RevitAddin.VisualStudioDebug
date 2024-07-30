# RevitAddin.VisualStudioDebug

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

The `RevitAddin.VisualStudioDebug` plugin allow to enable debug in the current section of Revit and Visual Studio.

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## Usage

In the `Add-Ins` tab the panel `Debug` is created with three buttons `Start`, `Event`, and `Stop`.

![Debug-Light](assets/Debug-Light.png)![Debug-Light-Event](assets/Debug-Light-Event.png)![Debug-Light-Stop](assets/Debug-Light-Stop.png)

![Debug-Dark](assets/Debug-Dark.png)![Debug-Dark-Event](assets/Debug-Dark-Event.png)![Debug-Dark-Stop](assets/Debug-Dark-Stop.png)

* `Start`: Start Debugging using Visual Studio process.
* `Event`: Start Debugging using Visual Studio process when an assembly is loaded in the `AppDomain`.
* `Stop`: Stop Debugging using Visual Studio process.

## Installation

* Download and install [RevitAddin.VisualStudioDebug.exe](../../releases/latest/download/RevitAddin.VisualStudioDebug.zip)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!