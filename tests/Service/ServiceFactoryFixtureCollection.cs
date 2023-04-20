// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
//
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ParkingSpace.Tests {
    [CollectionDefinition("api-context")]
    public class ProblemSolutionsCollection : ICollectionFixture<ServiceFactory> {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    // [CollectionDefinition("api-context")]
    // public class ApiFixtureCollection : ICollectionFixture<ServiceFactory> { }

    [CollectionDefinition("api-context")]
    public class BackendFixtureCollection : ICollectionFixture<ServiceFactory> { }
}