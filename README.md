# About
This program assists in the creation of convolutional neural network models for image classification. Models are generated with the Keras library, wrapped from Python in C# to allow use between languages. 

# Usage
Load a set of images into the program to be used in classification training. Once the model is built and training is complete, the weights can be saved as a Keras weights file (h5) and be used in either Python or C# (Keras.NET) applications.

You'll need the following NuGet packages installed along with .NET Framework 5.0:
  * Keras.NET
  * TensorFlow.NET
  * Newtonsoft.Json
  * Numpy.Bare
  * NumSharp.Bitmap
  * Python.Included (3.7.3.13)
  * SharpLearning.Common.Interfaces
  * SharpLearning.Containers
  * SharpLearning.CrossValidation
  * SharpLearning.Metrics
  
 [Here's a guide on how to install NuGet packages in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio)
 
 Here's what the program looks like:
 ![Demo screenshot](https://github.com/danielathome19/Classification-Model-Builder/blob/master/ClassificationModelBuilder/Demo.jpg?raw=true)

# Bugs/Features
Bugs are tracked using the GitHub Issue Tracker.

Please use the issue tracker for the following purpose:
  * To raise a bug request; do include specific details and label it appropriately.
  * To suggest any improvements in existing features.
  * To suggest new features or structures or applications.
