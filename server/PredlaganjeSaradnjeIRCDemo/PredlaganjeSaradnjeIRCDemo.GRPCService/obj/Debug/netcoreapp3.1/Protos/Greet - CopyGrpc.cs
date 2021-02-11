// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/greet - Copy.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace PredlaganjeSaradnjeIRCDemo.GRPCService {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class Greeter
  {
    static readonly string __ServiceName = "helloworld.Greeter";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest> __Marshaller_helloworld_HelloRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest.Parser));
    static readonly grpc::Marshaller<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> __Marshaller_helloworld_HelloReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply.Parser));
    static readonly grpc::Marshaller<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies> __Marshaller_helloworld_HelloReplies = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies.Parser));

    static readonly grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> __Method_SayHello = new grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SayHello",
        __Marshaller_helloworld_HelloRequest,
        __Marshaller_helloworld_HelloReply);

    static readonly grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> __Method_SayHellos = new grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "SayHellos",
        __Marshaller_helloworld_HelloRequest,
        __Marshaller_helloworld_HelloReply);

    static readonly grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies> __Method_SayHelloMoreTime = new grpc::Method<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SayHelloMoreTime",
        __Marshaller_helloworld_HelloRequest,
        __Marshaller_helloworld_HelloReplies);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::PredlaganjeSaradnjeIRCDemo.GRPCService.GreetCopyReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Greeter</summary>
    [grpc::BindServiceMethod(typeof(Greeter), "BindService")]
    public abstract partial class GreeterBase
    {
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> SayHello(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task SayHellos(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::IServerStreamWriter<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies> SayHelloMoreTime(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Greeter</summary>
    public partial class GreeterClient : grpc::ClientBase<GreeterClient>
    {
      /// <summary>Creates a new client for Greeter</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GreeterClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Greeter that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GreeterClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GreeterClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GreeterClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply SayHello(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHello(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply SayHello(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SayHello, null, options, request);
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> SayHelloAsync(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHelloAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> SayHelloAsync(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SayHello, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> SayHellos(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHellos(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply> SayHellos(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_SayHellos, null, options, request);
      }
      public virtual global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies SayHelloMoreTime(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHelloMoreTime(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies SayHelloMoreTime(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SayHelloMoreTime, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies> SayHelloMoreTimeAsync(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHelloMoreTimeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies> SayHelloMoreTimeAsync(global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SayHelloMoreTime, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GreeterClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GreeterClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GreeterBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SayHello, serviceImpl.SayHello)
          .AddMethod(__Method_SayHellos, serviceImpl.SayHellos)
          .AddMethod(__Method_SayHelloMoreTime, serviceImpl.SayHelloMoreTime).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GreeterBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SayHello, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply>(serviceImpl.SayHello));
      serviceBinder.AddMethod(__Method_SayHellos, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReply>(serviceImpl.SayHellos));
      serviceBinder.AddMethod(__Method_SayHelloMoreTime, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloRequest, global::PredlaganjeSaradnjeIRCDemo.GRPCService.HelloReplies>(serviceImpl.SayHelloMoreTime));
    }

  }
}
#endregion
