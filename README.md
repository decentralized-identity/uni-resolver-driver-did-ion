[![Build Status](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_apis/build/status/decentralized-identity.uni-resolver-driver-did-ion?branchName=master)](https://bmurdoch.visualstudio.com/uni-resolver-driver-did-ion/_build/latest?definitionId=1&branchName=master) ![Azure DevOps tests](https://img.shields.io/azure-devops/tests/bmurdoch/uni-resolver-driver-did-ion/1) ![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/bmurdoch/uni-resolver-driver-did-ion/1)

# uni-resolver-driver-did-ion
Universal Resolver Driver for Identity Overlay Network (ION) DIDs

![DIF Logo](https://raw.githubusercontent.com/decentralized-identity/universal-resolver/master/docs/logo-dif.png)

# Universal Resolver Driver: driver:ion

This is a [Universal Resolver](https://github.com/decentralized-identity/universal-resolver/) driver for **did:ion** identifiers.

## Specifications

* [Decentralized Identifiers](https://w3c.github.io/did-core/)
* [DID Method Specification](https://github.com/decentralized-identity/ion)

## Example DIDs

```
did:ion:test:EiAaxkdLcU9e78H4gKmA61I1A3BtS01Lwr1Ed7N3Xoy7gQ?-ion-initial-state=eyJ0eXBlIjoiY3JlYXRlIiwic3VmZml4RGF0YSI6ImV5SndZWFJqYUVSaGRHRklZWE5vSWpvaVJXbEJiak5zZFRSZlZrVnVkMlZoU0c5VFlqRlpNM3AxVWtKaVNGRjRMVkZaVEZBM1FXdHFkRkYxY0ZwUVVTSXNJbkpsWTI5MlpYSjVTMlY1SWpwN0ltdDBlU0k2SWtWRElpd2lZM0oySWpvaWMyVmpjREkxTm1zeElpd2llQ0k2SW1sS1ZHaHhibXRJVVV0eFdIcDZiMWxtZEUxVFJVeGZNRTFvWlV0SGEybERVelpWYTBkRWIwaDVjakFpTENKNUlqb2llR2h3TVdKd0xWbE1kbU5zYUhwSU9IaGhRWFZrYUhadWFEWkZaRVpPUjBwNmJYRmtWbHBVTmpKMVRTSjlMQ0p1WlhoMFVtVmpiM1psY25sRGIyMXRhWFJ0Wlc1MFNHRnphQ0k2SWtWcFFraGlabEZPT0ZwNk1sVkJlSEV3TFdsdFVIVk5lR2hHYUdWYVNIY3RWamRzTTFsM01IaEVUMEpZTkVFaWZRIiwicGF0Y2hEYXRhIjoiZXlKdVpYaDBWWEJrWVhSbFEyOXRiV2wwYldWdWRFaGhjMmdpT2lKRmFVSnNYM1pCY0hwSU5USmxTbU5MYTBkbFdYRkJWRmhyTFZReExXSjRhbDh6WkVkSE56UmtTM0ZVU20xUklpd2ljR0YwWTJobGN5STZXM3NpWVdOMGFXOXVJam9pY21Wd2JHRmpaU0lzSW1SdlkzVnRaVzUwSWpwN0luQjFZbXhwWTB0bGVYTWlPbHQ3SW1sa0lqb2ljMmxuYm1sdVowdGxlU0lzSW5SNWNHVWlPaUpUWldOd01qVTJhekZXWlhKcFptbGpZWFJwYjI1TFpYa3lNREU0SWl3aWFuZHJJanA3SW10MGVTSTZJa1ZESWl3aVkzSjJJam9pYzJWamNESTFObXN4SWl3aWVDSTZJa0ZGWVVGZlZFMXdUbk5TZDIxYVRuZGxOekI2TW5GZlpIb3hjbEUzUnpoblRqQmZWVUY1WkVWTmVWVWlMQ0o1SWpvaVNVTjZWalZEYVhGYVNtVkJVek0wZEVvMmREbEJkMHR2WlRWa1VYQnhiR1l5TlVWaGVUVlRkSEJqYnlKOWZWMHNJbk5sY25acFkyVkZibVJ3YjJsdWRITWlPbHQ3SW1sa0lqb2ljMlZ5ZG1salpVVnVaSEJ2YVc1MFNXUXhNak1pTENKMGVYQmxJam9pYzI5dFpWUjVjR1VpTENKelpYSjJhV05sUlc1a2NHOXBiblFpT2lKb2RIUndjem92TDNkM2R5NTFjbXd1WTI5dEluMWRmWDFkZlEifQ
```

## Build and Run (Docker)
```
docker build --rm -f "src\Dockerfile" -t uniresolverdriverdidion:latest "src"
docker run --rm -d  -p 5000:5000/tcp uniresolverdriverdidion:latest
curl -X GET http://localhost:5000/1.0/identifiers/did:ion:test:EiAaxkdLcU9e78H4gKmA61I1A3BtS01Lwr1Ed7N3Xoy7gQ?-ion-initial-state=eyJ0eXBlIjoiY3JlYXRlIiwic3VmZml4RGF0YSI6ImV5SndZWFJqYUVSaGRHRklZWE5vSWpvaVJXbEJiak5zZFRSZlZrVnVkMlZoU0c5VFlqRlpNM3AxVWtKaVNGRjRMVkZaVEZBM1FXdHFkRkYxY0ZwUVVTSXNJbkpsWTI5MlpYSjVTMlY1SWpwN0ltdDBlU0k2SWtWRElpd2lZM0oySWpvaWMyVmpjREkxTm1zeElpd2llQ0k2SW1sS1ZHaHhibXRJVVV0eFdIcDZiMWxtZEUxVFJVeGZNRTFvWlV0SGEybERVelpWYTBkRWIwaDVjakFpTENKNUlqb2llR2h3TVdKd0xWbE1kbU5zYUhwSU9IaGhRWFZrYUhadWFEWkZaRVpPUjBwNmJYRmtWbHBVTmpKMVRTSjlMQ0p1WlhoMFVtVmpiM1psY25sRGIyMXRhWFJ0Wlc1MFNHRnphQ0k2SWtWcFFraGlabEZPT0ZwNk1sVkJlSEV3TFdsdFVIVk5lR2hHYUdWYVNIY3RWamRzTTFsM01IaEVUMEpZTkVFaWZRIiwicGF0Y2hEYXRhIjoiZXlKdVpYaDBWWEJrWVhSbFEyOXRiV2wwYldWdWRFaGhjMmdpT2lKRmFVSnNYM1pCY0hwSU5USmxTbU5MYTBkbFdYRkJWRmhyTFZReExXSjRhbDh6WkVkSE56UmtTM0ZVU20xUklpd2ljR0YwWTJobGN5STZXM3NpWVdOMGFXOXVJam9pY21Wd2JHRmpaU0lzSW1SdlkzVnRaVzUwSWpwN0luQjFZbXhwWTB0bGVYTWlPbHQ3SW1sa0lqb2ljMmxuYm1sdVowdGxlU0lzSW5SNWNHVWlPaUpUWldOd01qVTJhekZXWlhKcFptbGpZWFJwYjI1TFpYa3lNREU0SWl3aWFuZHJJanA3SW10MGVTSTZJa1ZESWl3aVkzSjJJam9pYzJWamNESTFObXN4SWl3aWVDSTZJa0ZGWVVGZlZFMXdUbk5TZDIxYVRuZGxOekI2TW5GZlpIb3hjbEUzUnpoblRqQmZWVUY1WkVWTmVWVWlMQ0o1SWpvaVNVTjZWalZEYVhGYVNtVkJVek0wZEVvMmREbEJkMHR2WlRWa1VYQnhiR1l5TlVWaGVUVlRkSEJqYnlKOWZWMHNJbk5sY25acFkyVkZibVJ3YjJsdWRITWlPbHQ3SW1sa0lqb2ljMlZ5ZG1salpVVnVaSEJ2YVc1MFNXUXhNak1pTENKMGVYQmxJam9pYzI5dFpWUjVjR1VpTENKelpYSjJhV05sUlc1a2NHOXBiblFpT2lKb2RIUndjem92TDNkM2R5NTFjbXd1WTI5dEluMWRmWDFkZlEifQ
```
 
## Driver Environment Variables

`uni_resolver_driver_ion_timeout_in_milliseconds` request timeout for requests forward to the Microsoft beta discovery service  
`uni_resolver_driver_ion_url` the URL to the Microsoft beta discovery service

## Backlog

* Add unit tests
* Add support for HTTPS
* Add timeout and service configuration
* Add 'multi-request' to ION network