const { request, response } = require('express');

const { registerService } = require('../../services/auth/register.service.js');

/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.registerControllerApi = async function (req, res, next) {
  try {
    const { firstName, lastName, email, password, phone, company } = req.body;

    const token = await registerService(
      firstName,
      lastName,
      email,
      password,
      phone,
      company ?? ''
    );

    //if no errors
    res.status(201).json({
      success: true,
      jwt: token,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error,
    });
  }
};
