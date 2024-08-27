param webAppName string
param location string = resourceGroup().location

var appServicePlanName = '${webAppName}-asp'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-01-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1' // Free tier
    capacity: 1
  }
}

resource webApp 'Microsoft.Web/sites@2021-01-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      scmType: 'None' // Disable GitHub Actions
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource webAppAuthSettings 'Microsoft.Web/sites/config@2021-01-01' = {
  name: '${webApp.name}/authsettings'
  properties: {
    enabled: true
    unauthenticatedClientAction: 'RedirectToLoginPage'
    defaultProvider: 'AzureActiveDirectory'
    tokenStoreEnabled: true
  }
}
