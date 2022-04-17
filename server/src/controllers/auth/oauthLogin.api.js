const { request, response } = require('express');

const { oauthLoginSchema } = require('../../utils/dataValidator.util.js');

const jsonwebtoken = require('jsonwebtoken');

const Customer = require('../../../models/Customer.js');

/**
 *
 * @param {request} req
 * @param {response} res
 */
module.exports.oauthServiceApiController = async function (req, res, next) {
  const { email, firstName, lastName, userId } = req.body;

  console.log(userId);

  try {
    const { value, error } = oauthLoginSchema.validate({
      firstName: firstName,
      lastName: lastName,
      userId: userId,
      email: email,
    });

    console.log(value);
    console.log(error);

    if (error) {
      throw `${error}`;
    }

    //search for the user data is stored before or not

    let user = await Customer.findOne({
      oauthId: userId,
    });

    if (user) {
      const jwt = jsonwebtoken.sign(
        JSON.stringify(user),
        process.env.CLIENT_SECRET
      );
      res.status(201).json({
        success: true,
        jwt: jwt,
      });
    } else {
      user = await Customer.create({
        oauthId: userId,
        firstName: firstName,
        lastName: lastName,
        email: email,
      });
      const jwt = jsonwebtoken.sign(
        JSON.stringify(user),
        process.env.CLIENT_SECRET
      );
      res.status(201).json({
        success: true,
        jwt: jwt,
      });
    }
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error,
    });
  }
};
