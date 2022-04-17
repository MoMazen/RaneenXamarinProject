const { request, response } = require('express');

const { loginService } = require('../../services/auth/login.service.js');

/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.loginControllerApi = async function (req, res, next) {
  const { email, password } = req.body;

  if ((email, password)) {
    try {
      const jwt = await loginService(email, password);

      //if all are ok

      res.status(201).json({
        success: true,
        jwt: jwt,
      });
    } catch (error) {
      res.status(500).json({
        success: false,
        error: error,
      });
    }
  } else {
    res.status(500).json({
      success: false,
      error: 'email or password not provided',
    });
  }
};
