// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains the extension methods for <see cref="AspNetCore.Mvc.MvcOptions.Conventions"/>.
    /// </summary>
    public static class ApplicationModelConventionExtensions
    {
        /// <summary>
        /// Adds a <see cref="IControllerModelConvention"/> to all the controllers in the application.
        /// </summary>
        /// <param name="conventions">The list of <see cref="IApplicationModelConvention"/>
        /// in <see cref="AspNetCore.Mvc.MvcOptions"/>.</param>
        /// <param name="controllerModelConvention">The <see cref="IControllerModelConvention"/> which needs to be
        /// added.</param>
        public static void Add(
            this IList<IApplicationModelConvention> conventions,
            IControllerModelConvention controllerModelConvention)
        {
            if (conventions == null)
            {
                throw new ArgumentNullException(nameof(conventions));
            }

            if (controllerModelConvention == null)
            {
                throw new ArgumentNullException(nameof(controllerModelConvention));
            }

            conventions.Add(new ControllerApplicationModelConvention(controllerModelConvention));
        }

        /// <summary>
        /// Adds a <see cref="IActionModelConvention"/> to all the actions in the application.
        /// </summary>
        /// <param name="conventions">The list of <see cref="IApplicationModelConvention"/>
        /// in <see cref="AspNetCore.Mvc.MvcOptions"/>.</param>
        /// <param name="actionModelConvention">The <see cref="IActionModelConvention"/> which needs to be
        /// added.</param>
        public static void Add(
            this IList<IApplicationModelConvention> conventions,
            IActionModelConvention actionModelConvention)
        {
            if (conventions == null)
            {
                throw new ArgumentNullException(nameof(conventions));
            }

            if (actionModelConvention == null)
            {
                throw new ArgumentNullException(nameof(actionModelConvention));
            }

            conventions.Add(new ActionApplicationModelConvention(actionModelConvention));
        }

        /// <summary>
        /// Adds a <see cref="IParameterModelConvention"/> to all the parameters in the application.
        /// </summary>
        /// <param name="conventions">The list of <see cref="IApplicationModelConvention"/>
        /// in <see cref="AspNetCore.Mvc.MvcOptions"/>.</param>
        /// <param name="parameterModelConvention">The <see cref="IParameterModelConvention"/> which needs to be
        /// added.</param>
        public static void Add(
            this IList<IApplicationModelConvention> conventions,
            IParameterModelConvention parameterModelConvention)
        {
            if (conventions == null)
            {
                throw new ArgumentNullException(nameof(conventions));
            }

            if (parameterModelConvention == null)
            {
                throw new ArgumentNullException(nameof(parameterModelConvention));
            }

            conventions.Add(new ParameterApplicationModelConvention(parameterModelConvention));
        }

        private class ParameterApplicationModelConvention : IApplicationModelConvention
        {
            private readonly IParameterModelConvention _parameterModelConvention;

            public ParameterApplicationModelConvention(IParameterModelConvention parameterModelConvention)
            {
                if (parameterModelConvention == null)
                {
                    throw new ArgumentNullException(nameof(parameterModelConvention));
                }

                _parameterModelConvention = parameterModelConvention;
            }

            /// <inheritdoc />
            public void Apply(ApplicationModel application)
            {
                if (application == null)
                {
                    throw new ArgumentNullException(nameof(application));
                }

                foreach (var controller in application.Controllers)
                {
                    foreach (var action in controller.Actions)
                    {
                        foreach (var parameter in action.Parameters)
                        {
                            _parameterModelConvention.Apply(parameter);
                        }
                    }
                }
            }
        }

        private class ActionApplicationModelConvention : IApplicationModelConvention
        {
            private readonly IActionModelConvention _actionModelConvention;

            public ActionApplicationModelConvention(IActionModelConvention actionModelConvention)
            {
                if (actionModelConvention == null)
                {
                    throw new ArgumentNullException(nameof(actionModelConvention));
                }

                _actionModelConvention = actionModelConvention;
            }

            /// <inheritdoc />
            public void Apply(ApplicationModel application)
            {
                if (application == null)
                {
                    throw new ArgumentNullException(nameof(application));
                }

                foreach (var controller in application.Controllers)
                {
                    foreach (var action in controller.Actions)
                    {
                        _actionModelConvention.Apply(action);
                    }
                }
            }
        }

        private class ControllerApplicationModelConvention : IApplicationModelConvention
        {
            private readonly IControllerModelConvention _controllerModelConvention;

            public ControllerApplicationModelConvention(IControllerModelConvention controllerConvention)
            {
                if (controllerConvention == null)
                {
                    throw new ArgumentNullException(nameof(controllerConvention));
                }

                _controllerModelConvention = controllerConvention;
            }

            /// <inheritdoc />
            public void Apply(ApplicationModel application)
            {
                if (application == null)
                {
                    throw new ArgumentNullException(nameof(application));
                }

                foreach (var controller in application.Controllers)
                {
                    _controllerModelConvention.Apply(controller);
                }
            }
        }
    }
}