# NUnit2XUnit 

[![build](https://github.com/ku3mich/NUnit2XUnit/workflows/build/badge.svg)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=coverage)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=alert_status)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=bugs)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=code_smells)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=sqale_index)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=ku3mich_NUnit2XUnit&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=ku3mich_NUnit2XUnit)

Based on gist by Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).

https://gist.github.com/detunized/8d548bb3b6808f7f076ed1a5f2c6ddd4

### not supported:

- compile conditions such as #if SYMBOL are processed by default
- IgnoreAttribute
- ValuesAttribute for test argumets
- Sequential for test
- TearDown, SetUp, OneTimeSetUp, OneTimeTearDown
- Property (but Category supported)
- Constrait assertions(now support only Assert.That(smth, Is.Equal(smth)))

