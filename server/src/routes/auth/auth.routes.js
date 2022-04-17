const appRouter = require('express').Router();

const {
  registerControllerApi,
} = require('../../controllers/auth/registerControllerApi.api.js');

const {
  loginControllerApi,
} = require('../../controllers/auth/loginControllerApi.api.js');

const {
  oauthServiceApiController,
} = require('../../controllers/auth/oauthLogin.api.js');

appRouter.route('/api/v1/auth/register').post(registerControllerApi);
appRouter.route('/api/v1/auth/login').post(loginControllerApi);
appRouter.route('/api/v1/oauth/login').post(oauthServiceApiController);

module.exports = appRouter;
