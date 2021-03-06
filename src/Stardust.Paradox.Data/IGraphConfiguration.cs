﻿using System;
using System.Linq.Expressions;
using Stardust.Paradox.Data.Annotations;
using Stardust.Paradox.Data.Traversals;

namespace Stardust.Paradox.Data
{

    public interface IGraphConfiguration<T> where T : IVertex
    {
        IEdgeConfiguration<T> AddEdge(Expression<Func<T, object>> inPropertyLambda);
        IEdgeConfiguration<T> AddEdge(Expression<Func<T, object>> inPropertyLambda,bool eagerLoading);
        IEdgeConfiguration<T> AddEdge(Expression<Func<T, object>> inPropertyLambda, string label);
        IEdgeConfiguration<T> AddEdge(Expression<Func<T, object>> inPropertyLambda, string label, bool eagerLoading);
        IGraphConfiguration<T> AddInline(Expression<Func<T, object>> inPropertyLambda,  SerializationType serialization);

        IGraphConfiguration<T> AddQuery(Expression<Func<T, object>> inPropertyLambda, string gremlinQuery);

        IGraphConfiguration<T> AddQuery(Expression<Func<T, object>> inPropertyLambda, Func<GremlinContext, GremlinQuery> g);
        /// <summary>
        /// Start configuring a new collection. This method builds the entity, no further configuration is possible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <returns></returns>
        IGraphConfiguration<T> ConfigureCollection<T>(string label) where T : IVertex;
        /// <summary>
        /// Start configuring a new collection. This method builds the entity, no further configuration is possible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IGraphConfiguration<T> ConfigureCollection<T>() where T : IVertex;

        /// <summary>
        /// This method builds the entity, no further configuration is possible
        /// </summary>
        /// <returns></returns>
        //IGraphConfiguration BuildCollection();
    }

    public interface IEdgeConfiguration<T> where T : IVertex
    {
        /// <summary>
        ///  Binds the corresponding accessor property
        /// </summary>
        /// <typeparam name="TReverse"></typeparam>
        /// <param name="inPropertyLambda"></param>
        /// <returns></returns>
        IGraphConfiguration<T> Reverse<TReverse>(Expression<Func<TReverse, object>> inPropertyLambda);

        /// <summary>
        /// Binds the corresponding accessor property
        /// </summary>
        /// <typeparam name="TReverse"></typeparam>
        /// <param name="inPropertyLambda"></param>
        /// <param name="eagerLoading">Use with care, may cause entire graph to be loaded</param>
        /// <returns></returns>
        IGraphConfiguration<T> Reverse<TReverse>(Expression<Func<TReverse, object>> inPropertyLambda, bool eagerLoading);
        /// <summary>
        /// Start configuring a new collection. This method builds the entity, no further configuration is possible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <returns></returns>
        IGraphConfiguration<T> ConfigureCollection<T>(string label) where T : IVertex;

        /// <summary>
        /// Start configuring a new collection. This method builds the entity, no further configuration is possible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IGraphConfiguration<T> ConfigureCollection<T>() where T : IVertex;
        /// <summary>
        ///  This method builds the entity, no further configuration is possible
        /// </summary>
        /// <returns></returns>
        //IGraphConfiguration BuildCollection();


    }

    public interface IGraphConfiguration
    {
        //IGraphConfiguration AddCollection<T>() where T : IVertex;
        //IGraphConfiguration AddCollection<T>(string label) where T : IVertex;

        IGraphConfiguration<T> ConfigureCollection<T>(string label) where T : IVertex;

        IGraphConfiguration<T> ConfigureCollection<T>() where T : IVertex;

        void BuildModel();
    }
}