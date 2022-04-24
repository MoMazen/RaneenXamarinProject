const ApplicationRouter = require('express').Router();

const {
  getCategoriesControllerApi,
  seedCategoriesControllerApi,
} = require('../../controllers/categories/categories.controller.api.js');

ApplicationRouter.route('/api/v1/categories/add').post(
  seedCategoriesControllerApi
); //admin

ApplicationRouter.route('/api/v1/categories').get(getCategoriesControllerApi); //client

module.exports = ApplicationRouter;
